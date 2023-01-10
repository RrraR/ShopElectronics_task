using Microsoft.EntityFrameworkCore;
using ShopElectronics.Data.Entities;

namespace ShopElectronics.Data
{
    public partial class ShopElectronicsDbContext : DbContext
    {
        public ShopElectronicsDbContext(DbContextOptions<ShopElectronicsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public virtual DbSet<Category> Categories { get; set; } = null!;

        public virtual DbSet<Orders> Orders { get; set; } = null!;
        
        public virtual DbSet<OrderItems> OrderItems { get; set; } = null!;
        
        public virtual DbSet<OrderStatuses> OrderStatuses { get; set; } = null!;
        public virtual DbSet<Brands> Brands { get; set; } = null!;




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Cart)
                    .HasForeignKey<Cart>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_User");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartItem_Cart");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartItem_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductCategory");
                
                entity.HasOne(d => d.Brands)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Brands");
            });

            modelBuilder.Entity<User>(entity => { entity.Property(c => c.Id).ValueGeneratedOnAdd(); });

            modelBuilder.Entity<Category>(entity => { entity.Property(c => c.Id).ValueGeneratedOnAdd(); });


            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CategoryKeyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategory_Categories");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_User");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_OrderStatus");
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_Product");
            });
            
            modelBuilder.Entity<Brands>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OrderStatuses>(entity => { entity.Property(c => c.Id).ValueGeneratedOnAdd(); });


            // OnModelCreatingPartial(modelBuilder);
        }

        // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}