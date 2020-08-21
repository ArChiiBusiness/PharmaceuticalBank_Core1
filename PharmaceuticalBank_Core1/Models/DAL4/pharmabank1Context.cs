using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PharmaceuticalBank_Core1.Models.DAL4
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

        public virtual DbSet<AggregatedCounter> AggregatedCounter { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Counter> Counter { get; set; }
        public virtual DbSet<Hash> Hash { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobParameter> JobParameter { get; set; }
        public virtual DbSet<JobQueue> JobQueue { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<Phrases> Phrases { get; set; }
        public virtual DbSet<Schema> Schema { get; set; }
        public virtual DbSet<Server> Server { get; set; }
        public virtual DbSet<Set> Set { get; set; }
        public virtual DbSet<Shipments> Shipments { get; set; }
        public virtual DbSet<ShipmentsBackup> ShipmentsBackup { get; set; }
        public virtual DbSet<State> State { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=pharmabank.cerq8sfrnx31.us-east-1.rds.amazonaws.com;Initial Catalog=pb2;User ID=admin;Password=pharma111;Connection Timeout=1000");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_AggregatedCounter_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Dunsa).HasColumnName("DUNSA");

                entity.Property(e => e.Siccodes).HasColumnName("SICCodes");

                entity.Property(e => e.UltimateParentHeadquartersAddress).HasColumnName("Ultimate ParentHeadquartersAddress");

                entity.Property(e => e.UltimateParentStockTickers).HasColumnName("Ultimate Parent Stock Tickers");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => e.Key)
                    .HasName("CX_HangFire_Counter")
                    .IsClustered();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_Hash_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.StateName)
                    .HasName("IX_HangFire_Job_StateName")
                    .HasFilter("([StateName] IS NOT NULL)");

                entity.HasIndex(e => new { e.StateName, e.ExpireAt })
                    .HasName("IX_HangFire_Job_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameter)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_List_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Phrases>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Phrase).IsRequired();
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.HasIndex(e => e.LastHeartbeat)
                    .HasName("IX_HangFire_Server_LastHeartbeat");

                entity.Property(e => e.Id).HasMaxLength(100);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_Set_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => new { e.Key, e.Score })
                    .HasName("IX_HangFire_Set_Score");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Shipments>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Consignee).IsUnicode(false);

                entity.Property(e => e.ConsigneeAddress)
                    .HasColumnName("Consignee Address")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeCity)
                    .HasColumnName("Consignee City")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeCountry)
                    .HasColumnName("Consignee Country")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeDUNSâ)
                    .HasColumnName("Consignee D-U-N-SÂ®")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeEmail1)
                    .HasColumnName("Consignee Email 1")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeEmail2)
                    .HasColumnName("Consignee Email 2")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeEmail3)
                    .HasColumnName("Consignee Email 3")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeEmployees)
                    .HasColumnName("Consignee Employees")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeFax)
                    .HasColumnName("Consignee Fax")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeFullAddress)
                    .HasColumnName("Consignee Full Address")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeIndustry)
                    .HasColumnName("Consignee Industry")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeMarketCapitalization)
                    .HasColumnName("Consignee Market Capitalization")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneePhone1)
                    .HasColumnName("Consignee Phone 1")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneePhone2)
                    .HasColumnName("Consignee Phone 2")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneePhone3)
                    .HasColumnName("Consignee Phone 3")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneePostalCode)
                    .HasColumnName("Consignee Postal Code")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeProfile)
                    .HasColumnName("Consignee Profile")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeRevenue)
                    .HasColumnName("Consignee Revenue")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeSicCodes)
                    .HasColumnName("Consignee SIC Codes")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeStateRegion)
                    .HasColumnName("Consignee State Region")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeStockTickers)
                    .HasColumnName("Consignee Stock Tickers")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeTradeRoles)
                    .HasColumnName("Consignee Trade Roles")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeUltimateParent)
                    .HasColumnName("Consignee Ultimate Parent")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeUltimateParentHeadquartersAddress)
                    .HasColumnName("Consignee Ultimate Parent Headquarters Address")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeUltimateParentProfile)
                    .HasColumnName("Consignee Ultimate Parent Profile")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeUltimateParentStockTickers)
                    .HasColumnName("Consignee Ultimate Parent Stock Tickers")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeUltimateParentWebsite)
                    .HasColumnName("Consignee Ultimate Parent Website")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeWebsite1)
                    .HasColumnName("Consignee Website 1")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeWebsite2)
                    .HasColumnName("Consignee Website 2")
                    .IsUnicode(false);

                entity.Property(e => e.DataSource)
                    .HasColumnName("Data Source")
                    .IsUnicode(false);

                entity.Property(e => e.DataSourceTradeDirection)
                    .HasColumnName("Data Source Trade Direction")
                    .IsUnicode(false);

                entity.Property(e => e.GoodsShipped)
                    .HasColumnName("Goods Shipped")
                    .IsUnicode(false);

                entity.Property(e => e.HsCode)
                    .HasColumnName("HS Code")
                    .IsUnicode(false);

                entity.Property(e => e.IsContainerized)
                    .HasColumnName("Is Containerized")
                    .IsUnicode(false);

                entity.Property(e => e.MatchingFields)
                    .HasColumnName("Matching Fields")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfLading)
                    .HasColumnName("Port of Lading")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfLadingCountry)
                    .HasColumnName("Port of Lading Country")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfLadingUnLocode)
                    .HasColumnName("Port of Lading UN LOCODE")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfUnlading)
                    .HasColumnName("Port of Unlading")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfUnladingCountry)
                    .HasColumnName("Port of Unlading Country")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfUnladingUnLocode)
                    .HasColumnName("Port of Unlading UN LOCODE")
                    .IsUnicode(false);

                entity.Property(e => e.Scac)
                    .HasColumnName("SCAC")
                    .IsUnicode(false);

                entity.Property(e => e.ShipmentDestination)
                    .HasColumnName("Shipment Destination")
                    .IsUnicode(false);

                entity.Property(e => e.ShipmentOrigin)
                    .HasColumnName("Shipment Origin")
                    .IsUnicode(false);

                entity.Property(e => e.Shipper).IsUnicode(false);

                entity.Property(e => e.ShipperAddress)
                    .HasColumnName("Shipper Address")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperCity)
                    .HasColumnName("Shipper City")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperCountry)
                    .HasColumnName("Shipper Country")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperDUNSâ)
                    .HasColumnName("Shipper D-U-N-SÂ®")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperEmail1)
                    .HasColumnName("Shipper Email 1")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperEmail2)
                    .HasColumnName("Shipper Email 2")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperEmail3)
                    .HasColumnName("Shipper Email 3")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperEmployees)
                    .HasColumnName("Shipper Employees")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperFax)
                    .HasColumnName("Shipper Fax")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperFullAddress)
                    .HasColumnName("Shipper Full Address")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperIndustry)
                    .HasColumnName("Shipper Industry")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperMarketCapitalization)
                    .HasColumnName("Shipper Market Capitalization")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperPhone1)
                    .HasColumnName("Shipper Phone 1")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperPhone2)
                    .HasColumnName("Shipper Phone 2")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperPhone3)
                    .HasColumnName("Shipper Phone 3")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperPostalCode)
                    .HasColumnName("Shipper Postal Code")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperProfile)
                    .HasColumnName("Shipper Profile")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperRevenue)
                    .HasColumnName("Shipper Revenue")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperSicCodes)
                    .HasColumnName("Shipper SIC Codes")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperStateRegion)
                    .HasColumnName("Shipper State Region")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperStockTickers)
                    .HasColumnName("Shipper Stock Tickers")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperTradeRoles)
                    .HasColumnName("Shipper Trade Roles")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperUltimateParent)
                    .HasColumnName("Shipper Ultimate Parent")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperUltimateParentHeadquartersAddress)
                    .HasColumnName("Shipper Ultimate Parent Headquarters Address")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperUltimateParentProfile)
                    .HasColumnName("Shipper Ultimate Parent Profile")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperUltimateParentStockTickers)
                    .HasColumnName("Shipper Ultimate Parent Stock Tickers")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperUltimateParentWebsite)
                    .HasColumnName("Shipper Ultimate Parent Website")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperWebsite1)
                    .HasColumnName("Shipper Website 1")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperWebsite2)
                    .HasColumnName("Shipper Website 2")
                    .IsUnicode(false);

                entity.Property(e => e.TransportMethod)
                    .HasColumnName("Transport Method")
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("Type ")
                    .IsUnicode(false);

                entity.Property(e => e.ValueUsd)
                    .HasColumnName("Value (USD)")
                    .IsUnicode(false);

                entity.Property(e => e.VolumeTeu)
                    .HasColumnName("Volume (TEU)")
                    .IsUnicode(false);

                entity.Property(e => e.WeightKg)
                    .HasColumnName("Weight (KG)")
                    .IsUnicode(false);

                entity.HasOne(d => d.ConsigneeCompany)
                    .WithMany(p => p.ShipmentsConsigneeCompany)
                    .HasForeignKey(d => d.ConsigneeCompanyId)
                    .HasConstraintName("FK_Shipments_Companies");

                entity.HasOne(d => d.ShipperCompany)
                    .WithMany(p => p.ShipmentsShipperCompany)
                    .HasForeignKey(d => d.ShipperCompanyId)
                    .HasConstraintName("FK_Shipments_Companies1");
            });

            modelBuilder.Entity<ShipmentsBackup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("shipments_backup");

                entity.Property(e => e.Consignee).IsUnicode(false);

                entity.Property(e => e.ConsigneeAddress)
                    .HasColumnName("Consignee Address")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeCity)
                    .HasColumnName("Consignee City")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeCountry)
                    .HasColumnName("Consignee Country")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeDUNSâ)
                    .HasColumnName("Consignee D-U-N-SÂ®")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeEmail1)
                    .HasColumnName("Consignee Email 1")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeEmail2)
                    .HasColumnName("Consignee Email 2")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeEmail3)
                    .HasColumnName("Consignee Email 3")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeEmployees)
                    .HasColumnName("Consignee Employees")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeFax)
                    .HasColumnName("Consignee Fax")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeFullAddress)
                    .HasColumnName("Consignee Full Address")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeIndustry)
                    .HasColumnName("Consignee Industry")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeMarketCapitalization)
                    .HasColumnName("Consignee Market Capitalization")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneePhone1)
                    .HasColumnName("Consignee Phone 1")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneePhone2)
                    .HasColumnName("Consignee Phone 2")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneePhone3)
                    .HasColumnName("Consignee Phone 3")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneePostalCode)
                    .HasColumnName("Consignee Postal Code")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeProfile)
                    .HasColumnName("Consignee Profile")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeRevenue)
                    .HasColumnName("Consignee Revenue")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeSicCodes)
                    .HasColumnName("Consignee SIC Codes")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeStateRegion)
                    .HasColumnName("Consignee State Region")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeStockTickers)
                    .HasColumnName("Consignee Stock Tickers")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeTradeRoles)
                    .HasColumnName("Consignee Trade Roles")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeUltimateParent)
                    .HasColumnName("Consignee Ultimate Parent")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeUltimateParentHeadquartersAddress)
                    .HasColumnName("Consignee Ultimate Parent Headquarters Address")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeUltimateParentProfile)
                    .HasColumnName("Consignee Ultimate Parent Profile")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeUltimateParentStockTickers)
                    .HasColumnName("Consignee Ultimate Parent Stock Tickers")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeUltimateParentWebsite)
                    .HasColumnName("Consignee Ultimate Parent Website")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeWebsite1)
                    .HasColumnName("Consignee Website 1")
                    .IsUnicode(false);

                entity.Property(e => e.ConsigneeWebsite2)
                    .HasColumnName("Consignee Website 2")
                    .IsUnicode(false);

                entity.Property(e => e.DataSource)
                    .HasColumnName("Data Source")
                    .IsUnicode(false);

                entity.Property(e => e.DataSourceTradeDirection)
                    .HasColumnName("Data Source Trade Direction")
                    .IsUnicode(false);

                entity.Property(e => e.Date).IsUnicode(false);

                entity.Property(e => e.GoodsShipped)
                    .HasColumnName("Goods Shipped")
                    .IsUnicode(false);

                entity.Property(e => e.HsCode)
                    .HasColumnName("HS Code")
                    .IsUnicode(false);

                entity.Property(e => e.IsContainerized)
                    .HasColumnName("Is Containerized")
                    .IsUnicode(false);

                entity.Property(e => e.MatchingFields)
                    .HasColumnName("Matching Fields")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfLading)
                    .HasColumnName("Port of Lading")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfLadingCountry)
                    .HasColumnName("Port of Lading Country")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfLadingUnLocode)
                    .HasColumnName("Port of Lading UN LOCODE")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfUnlading)
                    .HasColumnName("Port of Unlading")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfUnladingCountry)
                    .HasColumnName("Port of Unlading Country")
                    .IsUnicode(false);

                entity.Property(e => e.PortOfUnladingUnLocode)
                    .HasColumnName("Port of Unlading UN LOCODE")
                    .IsUnicode(false);

                entity.Property(e => e.Scac)
                    .HasColumnName("SCAC")
                    .IsUnicode(false);

                entity.Property(e => e.ShipmentDestination)
                    .HasColumnName("Shipment Destination")
                    .IsUnicode(false);

                entity.Property(e => e.ShipmentOrigin)
                    .HasColumnName("Shipment Origin")
                    .IsUnicode(false);

                entity.Property(e => e.Shipper).IsUnicode(false);

                entity.Property(e => e.ShipperAddress)
                    .HasColumnName("Shipper Address")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperCity)
                    .HasColumnName("Shipper City")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperCountry)
                    .HasColumnName("Shipper Country")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperDUNSâ)
                    .HasColumnName("Shipper D-U-N-SÂ®")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperEmail1)
                    .HasColumnName("Shipper Email 1")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperEmail2)
                    .HasColumnName("Shipper Email 2")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperEmail3)
                    .HasColumnName("Shipper Email 3")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperEmployees)
                    .HasColumnName("Shipper Employees")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperFax)
                    .HasColumnName("Shipper Fax")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperFullAddress)
                    .HasColumnName("Shipper Full Address")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperIndustry)
                    .HasColumnName("Shipper Industry")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperMarketCapitalization)
                    .HasColumnName("Shipper Market Capitalization")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperPhone1)
                    .HasColumnName("Shipper Phone 1")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperPhone2)
                    .HasColumnName("Shipper Phone 2")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperPhone3)
                    .HasColumnName("Shipper Phone 3")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperPostalCode)
                    .HasColumnName("Shipper Postal Code")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperProfile)
                    .HasColumnName("Shipper Profile")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperRevenue)
                    .HasColumnName("Shipper Revenue")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperSicCodes)
                    .HasColumnName("Shipper SIC Codes")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperStateRegion)
                    .HasColumnName("Shipper State Region")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperStockTickers)
                    .HasColumnName("Shipper Stock Tickers")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperTradeRoles)
                    .HasColumnName("Shipper Trade Roles")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperUltimateParent)
                    .HasColumnName("Shipper Ultimate Parent")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperUltimateParentHeadquartersAddress)
                    .HasColumnName("Shipper Ultimate Parent Headquarters Address")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperUltimateParentProfile)
                    .HasColumnName("Shipper Ultimate Parent Profile")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperUltimateParentStockTickers)
                    .HasColumnName("Shipper Ultimate Parent Stock Tickers")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperUltimateParentWebsite)
                    .HasColumnName("Shipper Ultimate Parent Website")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperWebsite1)
                    .HasColumnName("Shipper Website 1")
                    .IsUnicode(false);

                entity.Property(e => e.ShipperWebsite2)
                    .HasColumnName("Shipper Website 2")
                    .IsUnicode(false);

                entity.Property(e => e.TransportMethod)
                    .HasColumnName("Transport Method")
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("Type ")
                    .IsUnicode(false);

                entity.Property(e => e.ValueUsd)
                    .HasColumnName("Value (USD)")
                    .IsUnicode(false);

                entity.Property(e => e.VolumeTeu)
                    .HasColumnName("Volume (TEU)")
                    .IsUnicode(false);

                entity.Property(e => e.WeightKg)
                    .HasColumnName("Weight (KG)")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

                entity.ToTable("State", "HangFire");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
