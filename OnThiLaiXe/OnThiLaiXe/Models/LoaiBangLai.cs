using System.ComponentModel.DataAnnotations;

namespace OnThiLaiXe.Models
{
    public class LoaiBangLai
    {
        public int Id { get; set; }

        [Required]
        public string TenLoai { get; set; }

        public string MoTa { get; set; }

        public ICollection<CauHoi> CauHois { get; set; }
    }
}
