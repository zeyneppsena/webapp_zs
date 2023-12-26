﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapp_ZS.Data;

#nullable disable

namespace webapp_ZS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231224101440_Migration_test")]
    partial class Migration_test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

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
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("webapp_ZS.Models.Artists", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("artistBiography")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("artistContact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("artistName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("webapp_ZS.Models.Artworks", b =>
                {
                    b.Property<int>("artworksId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("artworksId"), 1L, 1);

                    b.Property<string>("DosyaAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("artistId")
                        .HasColumnType("int");

                    b.Property<DateTime>("artworksCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("artworksDescripton")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("artworksDimensions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("artworksMaterials")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("artworksName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("artworksPrice")
                        .HasColumnType("float");

                    b.Property<int>("artworksStockQuantity")
                        .HasColumnType("int");

                    b.Property<int>("categoriesId")
                        .HasColumnType("int");

                    b.HasKey("artworksId");

                    b.HasIndex("artistId");

                    b.HasIndex("categoriesId");

                    b.ToTable("Artworks");
                });

            modelBuilder.Entity("webapp_ZS.Models.ArtworkTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("artworkId")
                        .HasColumnType("int");

                    b.Property<string>("tagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("artworkId");

                    b.ToTable("ArtworkTags");
                });

            modelBuilder.Entity("webapp_ZS.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("artworksId")
                        .HasColumnType("int");

                    b.Property<int>("cartPiece")
                        .HasColumnType("int");

                    b.Property<DateTime>("cartTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("cartTotalAmount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("artworksId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("webapp_ZS.Models.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("categoryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("webapp_ZS.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("artistId")
                        .HasColumnType("int");

                    b.Property<string>("eventDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("eventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("eventTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("artistId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("webapp_ZS.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("artworksId")
                        .HasColumnType("int");

                    b.Property<int>("cartPiece")
                        .HasColumnType("int");

                    b.Property<decimal>("cartTotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("customerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("saleAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("saleCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userIds")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("artworksId");

                    b.ToTable("Sales");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("webapp_ZS.Models.Artworks", b =>
                {
                    b.HasOne("webapp_ZS.Models.Artists", "Artists")
                        .WithMany("Artworks")
                        .HasForeignKey("artistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapp_ZS.Models.Categories", "Categories")
                        .WithMany("Artworks")
                        .HasForeignKey("categoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artists");

                    b.Navigation("Categories");
                });

            modelBuilder.Entity("webapp_ZS.Models.ArtworkTag", b =>
                {
                    b.HasOne("webapp_ZS.Models.Artworks", "Artworks")
                        .WithMany("ArtworkTags")
                        .HasForeignKey("artworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artworks");
                });

            modelBuilder.Entity("webapp_ZS.Models.Cart", b =>
                {
                    b.HasOne("webapp_ZS.Models.Artworks", "artworks")
                        .WithMany("Carts")
                        .HasForeignKey("artworksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("artworks");
                });

            modelBuilder.Entity("webapp_ZS.Models.Event", b =>
                {
                    b.HasOne("webapp_ZS.Models.Artists", "Artists")
                        .WithMany("Events")
                        .HasForeignKey("artistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artists");
                });

            modelBuilder.Entity("webapp_ZS.Models.Sale", b =>
                {
                    b.HasOne("webapp_ZS.Models.Cart", null)
                        .WithMany("Sales")
                        .HasForeignKey("CartId");

                    b.HasOne("webapp_ZS.Models.Artworks", "artworks")
                        .WithMany()
                        .HasForeignKey("artworksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("artworks");
                });

            modelBuilder.Entity("webapp_ZS.Models.Artists", b =>
                {
                    b.Navigation("Artworks");

                    b.Navigation("Events");
                });

            modelBuilder.Entity("webapp_ZS.Models.Artworks", b =>
                {
                    b.Navigation("ArtworkTags");

                    b.Navigation("Carts");
                });

            modelBuilder.Entity("webapp_ZS.Models.Cart", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("webapp_ZS.Models.Categories", b =>
                {
                    b.Navigation("Artworks");
                });
#pragma warning restore 612, 618
        }
    }
}
