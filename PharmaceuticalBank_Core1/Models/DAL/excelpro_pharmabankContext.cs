using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PharmaceuticalBank_Core1.Models.DAL
{
    public partial class excelpro_pharmabankContext : DbContext
    {
        public excelpro_pharmabankContext()
        {
        }

        public excelpro_pharmabankContext(DbContextOptions<excelpro_pharmabankContext> options)
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
        public virtual DbSet<Shipments> Shipments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=mssql.excelpro.business;Initial Catalog=excelpro_pharmabank;User ID=pharmaly_admin;Password=Pharmaly1%");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "pharmaly_admin");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasNoKey();

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

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LockoutEnd).HasColumnType("sql_variant");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Shipments>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Consignee).HasMaxLength(255);

                entity.Property(e => e.ConsigneeAddress)
                    .HasColumnName("Consignee Address")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeCity)
                    .HasColumnName("Consignee City")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeCountry)
                    .HasColumnName("Consignee Country")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeDUNSâ)
                    .HasColumnName("Consignee D-U-N-SÂ®")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeEmail1)
                    .HasColumnName("Consignee Email 1")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeEmail2)
                    .HasColumnName("Consignee Email 2")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeEmail3)
                    .HasColumnName("Consignee Email 3")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeEmployees).HasColumnName("Consignee Employees");

                entity.Property(e => e.ConsigneeFax)
                    .HasColumnName("Consignee Fax")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeFullAddress)
                    .HasColumnName("Consignee Full Address")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeIndustry)
                    .HasColumnName("Consignee Industry")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeMarketCapitalization)
                    .HasColumnName("Consignee Market Capitalization")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneePhone1)
                    .HasColumnName("Consignee Phone 1")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneePhone2)
                    .HasColumnName("Consignee Phone 2")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneePhone3)
                    .HasColumnName("Consignee Phone 3")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneePostalCode)
                    .HasColumnName("Consignee Postal Code")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeProfile)
                    .HasColumnName("Consignee Profile")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeRevenue)
                    .HasColumnName("Consignee Revenue")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeSicCodes)
                    .HasColumnName("Consignee SIC Codes")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeStateRegion)
                    .HasColumnName("Consignee State/Region")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeStockTickers)
                    .HasColumnName("Consignee Stock Tickers")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeTradeRoles)
                    .HasColumnName("Consignee Trade Roles")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeUltimateParent)
                    .HasColumnName("Consignee Ultimate Parent")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeUltimateParentHeadquartersAddress)
                    .HasColumnName("Consignee Ultimate Parent Headquarters Address")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeUltimateParentProfile)
                    .HasColumnName("Consignee Ultimate Parent Profile")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeUltimateParentStockTickers)
                    .HasColumnName("Consignee Ultimate Parent Stock Tickers")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeUltimateParentWebsite)
                    .HasColumnName("Consignee Ultimate Parent Website")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeWebsite1)
                    .HasColumnName("Consignee Website 1")
                    .HasMaxLength(255);

                entity.Property(e => e.ConsigneeWebsite2)
                    .HasColumnName("Consignee Website 2")
                    .HasMaxLength(255);

                entity.Property(e => e.DataSource)
                    .HasColumnName("Data Source")
                    .HasMaxLength(255);

                entity.Property(e => e.DataSourceTradeDirection)
                    .HasColumnName("Data Source Trade Direction")
                    .HasMaxLength(255);

                entity.Property(e => e.GoodsShipped).HasColumnName("Goods Shipped");

                entity.Property(e => e.HsCode)
                    .HasColumnName("HS Code")
                    .HasMaxLength(255);

                entity.Property(e => e.IsContainerized)
                    .HasColumnName("Is Containerized")
                    .HasMaxLength(255);

                entity.Property(e => e.MatchingFields)
                    .HasColumnName("Matching Fields")
                    .HasMaxLength(255);

                entity.Property(e => e.PortOfLading)
                    .HasColumnName("Port of Lading")
                    .HasMaxLength(255);

                entity.Property(e => e.PortOfLadingCountry)
                    .HasColumnName("Port of Lading Country")
                    .HasMaxLength(255);

                entity.Property(e => e.PortOfLadingUnLocode)
                    .HasColumnName("Port of Lading UN/LOCODE")
                    .HasMaxLength(255);

                entity.Property(e => e.PortOfUnlading)
                    .HasColumnName("Port of Unlading")
                    .HasMaxLength(255);

                entity.Property(e => e.PortOfUnladingCountry)
                    .HasColumnName("Port of Unlading Country")
                    .HasMaxLength(255);

                entity.Property(e => e.PortOfUnladingUnLocode)
                    .HasColumnName("Port of Unlading UN/LOCODE")
                    .HasMaxLength(255);

                entity.Property(e => e.Scac)
                    .HasColumnName("SCAC")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipmentDestination)
                    .HasColumnName("Shipment Destination")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipmentOrigin)
                    .HasColumnName("Shipment Origin")
                    .HasMaxLength(255);

                entity.Property(e => e.Shipper).HasMaxLength(255);

                entity.Property(e => e.ShipperAddress)
                    .HasColumnName("Shipper Address")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperCity)
                    .HasColumnName("Shipper City")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperCountry)
                    .HasColumnName("Shipper Country")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperDUNSâ).HasColumnName("Shipper D-U-N-SÂ®");

                entity.Property(e => e.ShipperEmail1)
                    .HasColumnName("Shipper Email 1")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperEmail2)
                    .HasColumnName("Shipper Email 2")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperEmail3)
                    .HasColumnName("Shipper Email 3")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperEmployees).HasColumnName("Shipper Employees");

                entity.Property(e => e.ShipperFax).HasColumnName("Shipper Fax");

                entity.Property(e => e.ShipperFullAddress)
                    .HasColumnName("Shipper Full Address")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperIndustry)
                    .HasColumnName("Shipper Industry")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperMarketCapitalization)
                    .HasColumnName("Shipper Market Capitalization")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperPhone1).HasColumnName("Shipper Phone 1");

                entity.Property(e => e.ShipperPhone2)
                    .HasColumnName("Shipper Phone 2")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperPhone3)
                    .HasColumnName("Shipper Phone 3")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperPostalCode)
                    .HasColumnName("Shipper Postal Code")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperProfile)
                    .HasColumnName("Shipper Profile")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperRevenue).HasColumnName("Shipper Revenue");

                entity.Property(e => e.ShipperSicCodes)
                    .HasColumnName("Shipper SIC Codes")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperStateRegion)
                    .HasColumnName("Shipper State/Region")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperStockTickers)
                    .HasColumnName("Shipper Stock Tickers")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperTradeRoles)
                    .HasColumnName("Shipper Trade Roles")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperUltimateParent)
                    .HasColumnName("Shipper Ultimate Parent")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperUltimateParentHeadquartersAddress)
                    .HasColumnName("Shipper Ultimate Parent Headquarters Address")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperUltimateParentProfile)
                    .HasColumnName("Shipper Ultimate Parent Profile")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperUltimateParentStockTickers)
                    .HasColumnName("Shipper Ultimate Parent Stock Tickers")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperUltimateParentWebsite)
                    .HasColumnName("Shipper Ultimate Parent Website")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperWebsite1)
                    .HasColumnName("Shipper Website 1")
                    .HasMaxLength(255);

                entity.Property(e => e.ShipperWebsite2)
                    .HasColumnName("Shipper Website 2")
                    .HasMaxLength(255);

                entity.Property(e => e.TransportMethod)
                    .HasColumnName("Transport Method")
                    .HasMaxLength(255);

                entity.Property(e => e.Type)
                    .HasColumnName("Type:")
                    .HasMaxLength(255);

                entity.Property(e => e.ValueUsd).HasColumnName("Value (USD)");

                entity.Property(e => e.VolumeTeu).HasColumnName("Volume (TEU)");

                entity.Property(e => e.WeightKg).HasColumnName("Weight (KG)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
