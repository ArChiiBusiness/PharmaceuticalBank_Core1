using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PharmaceuticalBank_Core1.Models.DAL
{
    public partial class pharmabank1Context : DbContext
    {
        public pharmabank1Context()
        {
        }

        public pharmabank1Context(DbContextOptions<pharmabank1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=pharmabank1;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AspNetRoleClaims", "pharmaly_admin");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AspNetRoles", "pharmaly_admin");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AspNetUserClaims", "pharmaly_admin");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AspNetUserLogins", "pharmaly_admin");

                entity.Property(e => e.LoginProvider)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ProviderKey)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AspNetUserRoles", "pharmaly_admin");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AspNetUserTokens", "pharmaly_admin");

                entity.Property(e => e.LoginProvider)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AspNetUsers", "pharmaly_admin");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LockoutEnd).HasColumnType("sql_variant");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Companies", "pharmaly_admin");

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.ContactEmail).HasMaxLength(255);

                entity.Property(e => e.ContactPerson).HasMaxLength(255);

                entity.Property(e => e.ContactPhone).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.DomesticHq)
                    .HasColumnName("DomesticHQ")
                    .HasMaxLength(255);

                entity.Property(e => e.DomesticHqaddress)
                    .HasColumnName("DomesticHQAddress")
                    .HasMaxLength(255);

                entity.Property(e => e.DomesticHqduns)
                    .HasColumnName("DomesticHQDUNS")
                    .HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FullAddress).HasMaxLength(255);

                entity.Property(e => e.GlobalHq)
                    .HasColumnName("GlobalHQ")
                    .HasMaxLength(255);

                entity.Property(e => e.GlobalHqaddress)
                    .HasColumnName("GlobalHQAddress")
                    .HasMaxLength(255);

                entity.Property(e => e.GlobalHqduns)
                    .HasColumnName("GlobalHQDUNS")
                    .HasMaxLength(255);

                entity.Property(e => e.LastShipmentDateOfMatchedShipments).HasColumnType("smalldatetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PanjivaUrl)
                    .HasColumnName("PanjivaURL")
                    .HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.PostalCode).HasMaxLength(255);

                entity.Property(e => e.Route).HasMaxLength(255);

                entity.Property(e => e.StateRegion).HasMaxLength(255);

                entity.Property(e => e.Top3Customers).HasMaxLength(255);

                entity.Property(e => e.Top5Products).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.Website).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
