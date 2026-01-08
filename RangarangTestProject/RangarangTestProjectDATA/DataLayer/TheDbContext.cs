using RangarangTestProjectDATA.Domain;
using Microsoft.EntityFrameworkCore;
using RangarangTestProjectDATA.Tools;
using System.Text.RegularExpressions;

namespace RangarangTestProjectDATA.DataLayer
{
    public class TheDbContext : DbContext
    {

        public TheDbContext(DbContextOptions<TheDbContext> options)
      : base(options)
        {
        }

        //public TheDbContext()
        //{

        //}

        //Tables

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Log> Logs { get; set; }


        public DbSet<Product> Products  { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<ProductMaterialAttribute> ProductMaterialAttributes { get; set; }
        public DbSet<ProductPrintKind> ProductPrintKinds { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }

        public DbSet<ProductAdt> ProductAdts { get; set; }
        public DbSet<ProductAdtType> ProductAdtTypes { get; set; }
        public DbSet<ProductAdtPrice> ProductAdtPrices { get; set; }

        public DbSet<ProductDeliver> ProductDelivers { get; set; }
        public DbSet<ProductDeliverSize> ProductDeliverSizes { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    MainDbConfigurationHelper configurationHelper = new MainDbConfigurationHelper();
        //    optionsBuilder.UseSqlServer(configurationHelper.GetConnectionString("publicdb"));
        //    //  base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product → ProductSize (Cascade OK)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Sizes)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product → ProductDeliver (Cascade OK)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Delivers)
                .WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductDeliver → ProductDeliverSize (Cascade)
            modelBuilder.Entity<ProductDeliver>()
                .HasMany(d => d.Sizes)
                .WithOne(ds => ds.ProductDeliver)
                .HasForeignKey(ds => ds.ProductDeliverId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductSize → ProductDeliverSize (NO ACTION)  ✅ این خط مشکل را حل می‌کند
            modelBuilder.Entity<ProductDeliverSize>()
                .HasOne(ds => ds.ProductSize)
                .WithMany(s => s.DeliverSizes)
                .HasForeignKey(ds => ds.ProductSizeId)
                .OnDelete(DeleteBehavior.NoAction);

            // --- Pricing (Restrict/NoAction) همان قبلی‌ها ---
            modelBuilder.Entity<ProductPrice>()
                .HasOne(pp => pp.ProductSize)
                .WithMany(s => s.Prices)
                .HasForeignKey(pp => pp.ProductSizeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductPrice>()
                .HasOne(pp => pp.ProductMaterial)
                .WithMany(m => m.Prices)
                .HasForeignKey(pp => pp.ProductMaterialId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductPrice>()
                .HasOne(pp => pp.ProductMaterialAttribute)
                .WithMany(a => a.Prices)
                .HasForeignKey(pp => pp.ProductMaterialAttributeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductPrice>()
                .HasOne(pp => pp.ProductPrintKind)
                .WithMany(pk => pk.Prices)
                .HasForeignKey(pp => pp.ProductPrintKindId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductAdtPrice>()
                .HasOne(ap => ap.ProductAdt)
                .WithMany(a => a.Prices)
                .HasForeignKey(ap => ap.ProductAdtId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductAdtPrice>()
                .HasOne(ap => ap.ProductAdtType)
                .WithMany(t => t.Prices)
                .HasForeignKey(ap => ap.ProductAdtTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductAdtPrice>()
                .HasOne(ap => ap.ProductPrice)
                .WithMany()
                .HasForeignKey(ap => ap.ProductPriceId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}