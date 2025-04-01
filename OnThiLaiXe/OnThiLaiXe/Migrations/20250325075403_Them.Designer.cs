﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnThiLaiXe.Models;

#nullable disable

namespace OnThiLaiXe.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250325075403_Them")]
    partial class Them
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OnThiLaiXe.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Age")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("OnThiLaiXe.Models.BaiThi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Diem")
                        .HasColumnType("int");

                    b.Property<bool>("MacLoiNghiemTrong")
                        .HasColumnType("bit");

                    b.Property<DateTime>("NgayThi")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiDungId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BaiThis");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.CauHoi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChuDeId")
                        .HasColumnType("int");

                    b.Property<string>("DapAnDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<bool>("DiemLiet")
                        .HasColumnType("bit");

                    b.Property<string>("GiaiThich")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoaiBangLaiId")
                        .HasColumnType("int");

                    b.Property<string>("LoaiMedia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LuaChonA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LuaChonB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LuaChonC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LuaChonD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MediaUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeoGhiNho")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChuDeId");

                    b.HasIndex("LoaiBangLaiId");

                    b.ToTable("CauHois");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.ChiTietBaiThi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaiThiId")
                        .HasColumnType("int");

                    b.Property<int>("CauHoiId")
                        .HasColumnType("int");

                    b.Property<string>("CauTraLoi")
                        .HasColumnType("nvarchar(1)");

                    b.Property<bool?>("DungSai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BaiThiId");

                    b.HasIndex("CauHoiId");

                    b.ToTable("ChiTietBaiThis");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.ChuDe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenChuDe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChuDes");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.KetQuaBaiThi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaiThiId")
                        .HasColumnType("int");

                    b.Property<int>("CauHoiId")
                        .HasColumnType("int");

                    b.Property<string>("CauTraLoi")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<bool>("DungSai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BaiThiId");

                    b.HasIndex("CauHoiId");

                    b.ToTable("KetQuaBaiThis");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.LoaiBangLai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LoaiBangLais");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OnThiLaiXe.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OnThiLaiXe.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnThiLaiXe.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OnThiLaiXe.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnThiLaiXe.Models.BaiThi", b =>
                {
                    b.HasOne("OnThiLaiXe.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.CauHoi", b =>
                {
                    b.HasOne("OnThiLaiXe.Models.ChuDe", "ChuDe")
                        .WithMany("CauHois")
                        .HasForeignKey("ChuDeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnThiLaiXe.Models.LoaiBangLai", "LoaiBangLai")
                        .WithMany("CauHois")
                        .HasForeignKey("LoaiBangLaiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChuDe");

                    b.Navigation("LoaiBangLai");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.ChiTietBaiThi", b =>
                {
                    b.HasOne("OnThiLaiXe.Models.BaiThi", "BaiThi")
                        .WithMany("ChiTietBaiThis")
                        .HasForeignKey("BaiThiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnThiLaiXe.Models.CauHoi", "CauHoi")
                        .WithMany("ChiTietBaiThis")
                        .HasForeignKey("CauHoiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaiThi");

                    b.Navigation("CauHoi");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.KetQuaBaiThi", b =>
                {
                    b.HasOne("OnThiLaiXe.Models.BaiThi", "BaiThi")
                        .WithMany()
                        .HasForeignKey("BaiThiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnThiLaiXe.Models.CauHoi", "CauHoi")
                        .WithMany()
                        .HasForeignKey("CauHoiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaiThi");

                    b.Navigation("CauHoi");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.BaiThi", b =>
                {
                    b.Navigation("ChiTietBaiThis");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.CauHoi", b =>
                {
                    b.Navigation("ChiTietBaiThis");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.ChuDe", b =>
                {
                    b.Navigation("CauHois");
                });

            modelBuilder.Entity("OnThiLaiXe.Models.LoaiBangLai", b =>
                {
                    b.Navigation("CauHois");
                });
#pragma warning restore 612, 618
        }
    }
}
