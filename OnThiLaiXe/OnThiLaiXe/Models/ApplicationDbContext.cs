﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnThiLaiXe.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ChuDe> ChuDes { get; set; }
        public DbSet<LoaiBangLai> LoaiBangLais { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<BaiThi> BaiThis { get; set; }
        public DbSet<ChiTietBaiThi> ChiTietBaiThis { get; set; }
        public DbSet<KetQuaBaiThi> KetQuaBaiThis { get; set; }
        public DbSet<CauHoiSai> CauHoiSais { get; set; } // 🔥 Thêm dòng này
    }


}
