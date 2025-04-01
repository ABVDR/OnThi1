namespace OnThiLaiXe.Models
{
    public class BaiThi
    {
        public int Id { get; set; }
        public DateTime NgayThi { get; set; }
        public string UserId { get; set; } // Thêm UserId để phân quyền

        // Navigation properties
        public virtual ICollection<ChiTietBaiThi> ChiTietBaiThis { get; set; }
        public virtual ApplicationUser User { get; set; } // Liên kết với người dùng
    }
}
