namespace OnThiLaiXe.Models
{
    public class BaiThiViewModel
    {
        public int BaiThiId { get; set; }
        public string NguoiDungId { get; set; }
        public DateTime NgayThi { get; set; }
        public int Diem { get; set; } // Thêm thuộc tính Diem để lưu điểm số người dùng đạt được

        public List<CauHoiViewModel> CauHois { get; set; }
        public Dictionary<int, char?> DapAnNguoiDung { get; set; } // Lưu đáp án của người dùng
    }




}
