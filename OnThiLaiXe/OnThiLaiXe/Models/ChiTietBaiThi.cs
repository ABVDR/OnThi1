namespace OnThiLaiXe.Models
{
    public class ChiTietBaiThi
    {
        public int Id { get; set; }

        public int BaiThiId { get; set; }
        public BaiThi BaiThi { get; set; }

        public int CauHoiId { get; set; }
        public CauHoi CauHoi { get; set; }

        public char? CauTraLoi { get; set; }

        public bool? DungSai { get; set; }
    }
}
