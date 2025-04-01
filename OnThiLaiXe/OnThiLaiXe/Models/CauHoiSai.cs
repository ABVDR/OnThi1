namespace OnThiLaiXe.Models
{
    public class CauHoiSai
    {
            public int Id { get; set; }

            public int UserId { get; set; } // Người dùng trả lời sai

            public int CauHoiId { get; set; }
            public CauHoi CauHoi { get; set; }

            public DateTime NgaySai { get; set; } = DateTime.Now;
        }

    
}
