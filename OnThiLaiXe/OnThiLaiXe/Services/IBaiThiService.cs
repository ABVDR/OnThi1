using OnThiLaiXe.Models;

namespace OnThiLaiXe.Services
{
    public interface IBaiThiService
    {
        Task<int?> TaoDeThiAsync(int loaiBangLaiId, Dictionary<int, int> soLuongMoiChuDe, string userId);
        Task<BaiThi> GetBaiThiByIdAsync(int id, string userId);
        Task<PaginatedList<BaiThi>> GetDanhSachBaiThiPaginatedAsync(string userId, int page, int pageSize);
        Task<BaiThi> GetBaiThiForThiAsync(int id, string userId);
        Task<List<KetQuaBaiThi>> NopBaiThiAsync(int baiThiId, IFormCollection form, string userId);
        Task<PaginatedList<BaiThi>> GetDanhSachDeThiPaginatedAsync(int page, int pageSize);
        Task<OnTapViewModel> GetOnTapViewModelAsync(int loaiBangLaiId);
        Task<OnTapTheoChuDeViewModel> GetOnTapTheoChuDeViewModelAsync(int? loaiBangLaiId);
        Task<bool> XoaBaiThiAsync(int id, string userId);
    }
}
