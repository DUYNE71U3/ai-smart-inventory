using Microsoft.EntityFrameworkCore;
using smart_inventory.Models;

namespace smart_inventory.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockDetail> StockDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Configure Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.SKU).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.Notes).HasMaxLength(500);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                
                // Unique constraint for SKU
                entity.HasIndex(e => e.SKU).IsUnique();
            });

            // Configure Stock
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DocumentNo).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.DocumentNo).IsUnique();
            });

            // Configure StockDetail
            modelBuilder.Entity<StockDetail>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                
                entity.HasOne(d => d.Stock)
                    .WithMany(s => s.Details)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed initial data với DateTime tĩnh
            var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            
            modelBuilder.Entity<Category>().HasData(
                new Category 
                { 
                    Id = 1, 
                    Name = "CPU", 
                    Description = "Bộ vi xử lý - Central Processing Unit",
                    CreatedAt = seedDate,
                    UpdatedAt = seedDate
                },
                new Category 
                { 
                    Id = 2, 
                    Name = "RAM", 
                    Description = "Bộ nhớ trong - Random Access Memory",
                    CreatedAt = seedDate,
                    UpdatedAt = seedDate
                },
                new Category 
                { 
                    Id = 3, 
                    Name = "Mainboard", 
                    Description = "Bo mạch chủ - Motherboard",
                    CreatedAt = seedDate,
                    UpdatedAt = seedDate
                },
                new Category 
                { 
                    Id = 4, 
                    Name = "Màn hình", 
                    Description = "Thiết bị hiển thị - Monitor",
                    CreatedAt = seedDate,
                    UpdatedAt = seedDate
                },
                new Category 
                { 
                    Id = 5, 
                    Name = "Bàn phím", 
                    Description = "Thiết bị nhập liệu - Keyboard",
                    CreatedAt = seedDate,
                    UpdatedAt = seedDate
                },
                new Category 
                { 
                    Id = 6, 
                    Name = "Chuột", 
                    Description = "Thiết bị con trỏ - Mouse",
                    CreatedAt = seedDate,
                    UpdatedAt = seedDate
                },
                new Category 
                { 
                    Id = 7, 
                    Name = "Ổ cứng", 
                    Description = "Thiết bị lưu trữ - Storage Drive",
                    CreatedAt = seedDate,
                    UpdatedAt = seedDate
                }
            );
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Category || e.Entity is Product)
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Category category)
                    {
                        category.CreatedAt = DateTime.Now;
                        category.UpdatedAt = DateTime.Now;
                    }
                    else if (entry.Entity is Product product)
                    {
                        product.CreatedAt = DateTime.Now;
                        product.UpdatedAt = DateTime.Now;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Category category)
                    {
                        category.UpdatedAt = DateTime.Now;
                    }
                    else if (entry.Entity is Product product)
                    {
                        product.UpdatedAt = DateTime.Now;
                    }
                }
            }
        }
    }
}