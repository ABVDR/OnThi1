using Microsoft.EntityFrameworkCore;
using OnThiLaiXe.Models;

namespace OnThiLaiXe.Services
{
    public class BaiThiService : IBaiThiService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BaiThiService> _logger;

        public BaiThiService(ApplicationDbContext context, ILogger<BaiThiService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int?> TaoDeThiAsync(int loaiBangLaiId, Dictionary<int, int> soLuongMoiChuDe, string userId)
        {
            var danhSachCauHoi = new List<CauHoi>();

            // Sử dụng transaction để đảm bảo tính nhất quán
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var chuDe in soLuongMoiChuDe)
                {
                    int chuDeId = chuDe.Key;
                    int soLuong = chuDe.Value;

                    if (soLuong <= 0)
                        continue;

                    // Lấy danh sách IDs của các câu hỏi phù hợp
                    var cauHoiIds = await _context.CauHois
                        .Where(c => c.LoaiBangLaiId == loaiBangLaiId && c.ChuDeId == chuDeId)
                        .Select(c => c.Id)
                        .ToListAsync();

                    // Lấy ngẫu nhiên bằng cách shuffle và lấy số lượng cần thiết
                    var selectedIds = ShuffleAndTake(cauHoiIds, soLuong);

                    if (selectedIds.Any())
                    {
                        var cauHoiTheoChuDe = await _context.CauHois
                            .Where(c => selectedIds.Contains(c.Id))
                            .ToListAsync();

                        danhSachCauHoi.AddRange(cauHoiTheoChuDe);
                    }
                }

                if (!danhSachCauHoi.Any())
                {
                    return null;
                }

                var deThi = new BaiThi
                {
                    NgayThi = DateTime.Now,
                    UserId = userId, // Lưu ID người dùng để kiểm soát quyền
                    ChiTietBaiThis = danhSachCauHoi.Select(c => new ChiTietBaiThi { CauHoiId = c.Id }).ToList()
                };

                _context.BaiThis.Add(deThi);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return deThi.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Lỗi khi tạo đề thi");
                return null;
            }
        }

        public async Task<BaiThi> GetBaiThiByIdAsync(int id, string userId)
        {
            return await _context.BaiThis
                .Include(bt => bt.ChiTietBaiThis)
                .ThenInclude(ct => ct.CauHoi)
                .FirstOrDefaultAsync(bt => bt.Id == id && bt.UserId == userId);
        }

        public async Task<PaginatedList<BaiThi>> GetDanhSachBaiThiPaginatedAsync(string userId, int page, int pageSize)
        {
            var query = _context.BaiThis
                .Include(bt => bt.ChiTietBaiThis)
                .Where(bt => bt.UserId == userId)
                .OrderByDescending(bt => bt.NgayThi);

            return await PaginatedList<BaiThi>.CreateAsync(query, page, pageSize);
        }

        public async Task<BaiThi> GetBaiThiForThiAsync(int id, string userId)
        {
            return await _context.BaiThis
                .Include(bt => bt.ChiTietBaiThis)
                .ThenInclude(ct => ct.CauHoi)
                .FirstOrDefaultAsync(bt => bt.Id == id && bt.UserId == userId);
        }

        public async Task<List<KetQuaBaiThi>> NopBaiThiAsync(int baiThiId, IFormCollection form, string userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var baiThi = await _context.BaiThis
                    .Include(bt => bt.ChiTietBaiThis)
                    .ThenInclude(ct => ct.CauHoi)
                    .FirstOrDefaultAsync(bt => bt.Id == baiThiId && bt.UserId == userId);

                if (baiThi == null)
                {
                    return null;
                }

                // Xóa kết quả cũ nếu có
                var ketQuaCu = await _context.KetQuaBaiThis
                    .Where(kq => kq.BaiThiId == baiThiId)
                    .ToListAsync();

                if (ketQuaCu.Any())
                {
                    _context.KetQuaBaiThis.RemoveRange(ketQuaCu);
                    await _context.SaveChangesAsync();
                }

                var dapAnNguoiDung = new Dictionary<int, char?>();
                foreach (var key in form.Keys)
                {
                    if (key.StartsWith("dapAn_") && int.TryParse(key.Split('_')[1], out int cauHoiId))
                    {
                        string dapAn = form[key].ToString();
                        if (!string.IsNullOrEmpty(dapAn) && dapAn.Length == 1)
                        {
                            dapAnNguoiDung[cauHoiId] = dapAn[0];
                        }
                    }
                }

                // Tạo danh sách kết quả để lưu
                var ketQua = new List<KetQuaBaiThi>();
                foreach (var chiTiet in baiThi.ChiTietBaiThis)
                {
                    var cauHoi = chiTiet.CauHoi;
                    char traLoi = '\0';
                    bool dungSai = false;

                    if (dapAnNguoiDung.TryGetValue(cauHoi.Id, out var answer) && answer.HasValue)
                    {
                        traLoi = answer.Value;
                        dungSai = traLoi == cauHoi.DapAnDung;
                    }

                    ketQua.Add(new KetQuaBaiThi
                    {
                        BaiThiId = baiThiId,
                        CauHoiId = cauHoi.Id,
                        CauTraLoi = traLoi,
                        DungSai = dungSai
                    });
                }

                // Lưu kết quả vào database theo batch
                await _context.KetQuaBaiThis.AddRangeAsync(ketQua);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Lấy lại kết quả với đầy đủ thông tin
                var ketQuaWithDetails = await _context.KetQuaBaiThis
                    .Include(kq => kq.CauHoi)
                    .Where(kq => kq.BaiThiId == baiThiId)
                    .ToListAsync();

                return ketQuaWithDetails;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Lỗi khi nộp bài thi");
                throw;
            }
        }

        public async Task<PaginatedList<BaiThi>> GetDanhSachDeThiPaginatedAsync(int page, int pageSize)
        {
            var query = _context.BaiThis
                .Include(bt => bt.ChiTietBaiThis)
                .Select(bt => new BaiThi
                {
                    Id = bt.Id,
                    NgayThi = bt.NgayThi,
                    UserId = bt.UserId,
                    ChiTietBaiThis = bt.ChiTietBaiThis.Select(ct => new ChiTietBaiThi
                    {
                        Id = ct.Id,
                        BaiThiId = ct.BaiThiId,
                        CauHoiId = ct.CauHoiId
                    }).ToList()
                })
                .OrderByDescending(bt => bt.NgayThi);

            return await PaginatedList<BaiThi>.CreateAsync(query, page, pageSize);
        }

        public async Task<OnTapViewModel> GetOnTapViewModelAsync(int loaiBangLaiId)
        {
            var loaiBangLai = await _context.LoaiBangLais.FindAsync(loaiBangLaiId);

            var cauHoiList = await _context.CauHois
                .Where(c => c.LoaiBangLaiId == loaiBangLaiId)
                .OrderBy(c => c.ChuDeId)
                .ToListAsync();

            return new OnTapViewModel
            {
                LoaiBangLai = loaiBangLai,
                CauHoiList = cauHoiList
            };
        }

        public async Task<OnTapTheoChuDeViewModel> GetOnTapTheoChuDeViewModelAsync(int? loaiBangLaiId)
        {
            var viewModel = new OnTapTheoChuDeViewModel
            {
                DanhSachChuDe = await _context.ChuDes.ToListAsync(),
                DanhSachLoaiBangLai = await _context.LoaiBangLais.ToListAsync(),
                SelectedLoaiBangLaiId = loaiBangLaiId
            };

            return viewModel;
        }

        public async Task<bool> XoaBaiThiAsync(int id, string userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Xóa các kết quả bài thi trước
                var ketQua = await _context.KetQuaBaiThis
                    .Where(kq => kq.BaiThiId == id)
                    .ToListAsync();

                if (ketQua.Any())
                {
                    _context.KetQuaBaiThis.RemoveRange(ketQua);
                    await _context.SaveChangesAsync();
                }

                // Tìm bài thi và kiểm tra quyền
                var baiThi = await _context.BaiThis
                    .Include(bt => bt.ChiTietBaiThis)
                    .FirstOrDefaultAsync(bt => bt.Id == id && bt.UserId == userId);

                if (baiThi == null)
                {
                    return false;
                }

                _context.BaiThis.Remove(baiThi);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Lỗi khi xóa bài thi");
                throw;
            }
        }

        // Phương thức helper để shuffle list và lấy n phần tử đầu tiên
        private List<int> ShuffleAndTake(List<int> list, int count)
        {
            if (list == null || !list.Any() || count <= 0)
                return new List<int>();

            count = Math.Min(count, list.Count);

            // Fisher-Yates shuffle algorithm
            var rng = new Random();
            var shuffled = new List<int>(list);

            int n = shuffled.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = shuffled[k];
                shuffled[k] = shuffled[n];
                shuffled[n] = value;
            }

            return shuffled.Take(count).ToList();
        }
    }
}