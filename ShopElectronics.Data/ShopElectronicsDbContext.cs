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
            });
            
            modelBuilder.Entity<User>(entity => { entity.Property(c => c.Id).ValueGeneratedOnAdd(); });
            
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });
            

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CategoryKeyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategory_Categories");
            });

            // OnModelCreatingPartial(modelBuilder);
        }

        // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}