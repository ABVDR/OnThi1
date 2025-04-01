using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnThiLaiXe.Models;
using OnThiLaiXe.Repositories;
using OnThiLaiXe.Services;
using OnThiLaiXe.ViewModels;

namespace OnThiLaiXe.Controllers
{
    [Authorize] // Đảm bảo chỉ người dùng đã xác thực mới truy cập được
    public class BaiThiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICauHoiRepository _cauHoiRepo;
        private readonly IBaiThiService _baiThiService;
        private readonly ILogger<BaiThiController> _logger;

        public BaiThiController(
            ApplicationDbContext context,
            ICauHoiRepository cauHoiRepo,
            IBaiThiService baiThiService,
            ILogger<BaiThiController> logger)
        {
            _context = context;
            _cauHoiRepo = cauHoiRepo;
            _baiThiService = baiThiService;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Ngăn chặn CSRF
        public async Task<IActionResult> TaoDeThi(int loaiBangLaiId, Dictionary<int, int> soLuongMoiChuDe)
        {
            try
            {
                // Validate đầu vào
                if (soLuongMoiChuDe == null || !soLuongMoiChuDe.Any())
                {
                    TempData["Error"] = "Số lượng câu hỏi theo chủ đề không hợp lệ.";
                    return RedirectToAction("ChonDeThi");
                }

                var loaiBangLai = await _context.LoaiBangLais.FindAsync(loaiBangLaiId);
                if (loaiBangLai == null)
                {
                    TempData["Error"] = "Loại bằng lái không hợp lệ.";
                    return RedirectToAction("ChonDeThi");
                }

                // Kiểm tra tổng số câu hỏi không vượt quá giới hạn
                int tongSoCauHoi = soLuongMoiChuDe.Values.Sum();
                if (tongSoCauHoi <= 0 || tongSoCauHoi > 100) // Giả sử tối đa 100 câu hỏi
                {
                    TempData["Error"] = "Tổng số câu hỏi phải từ 1 đến 100.";
                    return RedirectToAction("ChonDeThi");
                }

                // Sử dụng service để tạo đề thi
                int? deThiId = await _baiThiService.TaoDeThiAsync(loaiBangLaiId, soLuongMoiChuDe, GetCurrentUserId());

                if (!deThiId.HasValue)
                {
                    TempData["Error"] = "Không đủ câu hỏi để tạo đề thi.";
                    return RedirectToAction("ChonDeThi");
                }

                return RedirectToAction("ChiTietBaiThi", new { id = deThiId.Value });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo đề thi: {Message}", ex.Message);
                TempData["Error"] = "Đã xảy ra lỗi khi tạo đề thi. Vui lòng thử lại sau.";
                return RedirectToAction("ChonDeThi");
            }
        }

        public async Task<IActionResult> ChonDeThi()
        {
            var viewModel = new ChonDeThiViewModel
            {
                DanhSachChuDe = await _context.ChuDes.ToListAsync(),
                DanhSachLoaiBangLai = await _context.LoaiBangLais.ToListAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ChiTietBaiThi(int id)
        {
            var baiThi = await _baiThiService.GetBaiThiByIdAsync(id, GetCurrentUserId());

            if (baiThi == null)
            {
                return NotFound();
            }

            return View(baiThi);
        }

        public async Task<IActionResult> DanhSachBaiThi(int page = 1, int pageSize = 10)
        {
            var viewModel = await _baiThiService.GetDanhSachBaiThiPaginatedAsync(GetCurrentUserId(), page, pageSize);
            return View(viewModel);
        }

        public async Task<IActionResult> LamBaiThi(int id)
        {
            var baiThi = await _baiThiService.GetBaiThiForThiAsync(id, GetCurrentUserId());

            if (baiThi == null)
            {
                return NotFound();
            }

            return View(baiThi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Ngăn chặn CSRF
        public async Task<IActionResult> NopBaiThi(int baiThiId, IFormCollection form)
        {
            try
            {
                if (baiThiId <= 0)
                {
                    return BadRequest("ID bài thi không hợp lệ.");
                }

                var ketQua = await _baiThiService.NopBaiThiAsync(baiThiId, form, GetCurrentUserId());

                if (ketQua == null)
                {
                    return NotFound("Không tìm thấy bài thi hoặc bài thi không thuộc về bạn.");
                }

                return View("KetQuaBaiThi", ketQua);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi nộp bài thi: {Message}", ex.Message);
                TempData["Error"] = "Đã xảy ra lỗi khi nộp bài thi. Vui lòng thử lại.";
                return RedirectToAction("LamBaiThi", new { id = baiThiId });
            }
        }

        public async Task<IActionResult> DanhSachDeThi(int page = 1, int pageSize = 10)
        {
            var viewModel = await _baiThiService.GetDanhSachDeThiPaginatedAsync(page, pageSize);
            return View(viewModel);
        }

        public async Task<IActionResult> OnTap(int loaiBangLaiId)
        {
            if (!await _context.LoaiBangLais.AnyAsync(l => l.Id == loaiBangLaiId))
            {
                TempData["Error"] = "Loại bằng lái không hợp lệ.";
                return RedirectToAction("DanhSachLoaiBangLai");
            }

            var viewModel = await _baiThiService.GetOnTapViewModelAsync(loaiBangLaiId);

            if (viewModel.CauHoiList.Count == 0)
            {
                TempData["Error"] = "Không có câu hỏi nào cho loại bằng lái này.";
                return RedirectToAction("DanhSachLoaiBangLai");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DanhSachLoaiBangLai()
        {
            var danhSachLoaiBangLai = await _context.LoaiBangLais.ToListAsync();
            return View(danhSachLoaiBangLai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Ngăn chặn CSRF
        public async Task<IActionResult> XoaBaiThi(int id)
        {
            try
            {
                var result = await _baiThiService.XoaBaiThiAsync(id, GetCurrentUserId());

                if (!result)
                {
                    return NotFound("Không tìm thấy bài thi hoặc bài thi không thuộc về bạn.");
                }

                TempData["Success"] = "Xóa bài thi thành công.";
                return RedirectToAction("DanhSachBaiThi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa bài thi: {Message}", ex.Message);
                TempData["Error"] = "Đã xảy ra lỗi khi xóa bài thi.";
                return RedirectToAction("DanhSachBaiThi");
            }
        }

        public async Task<IActionResult> OnTapTheoChuDeVaLoaiBangLai(int? loaiBangLaiId)
        {
            var viewModel = await _baiThiService.GetOnTapTheoChuDeViewModelAsync(loaiBangLaiId);
            return View(viewModel);
        }

        // Helper method to get current user ID
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}