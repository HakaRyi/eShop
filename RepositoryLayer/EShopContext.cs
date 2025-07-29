using BOs.Entities;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class EShopContext : DbContext
    {
        public EShopContext(DbContextOptions<EShopContext> options) : base(options)
        {

            this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships and properties here
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // Member - Order (1-n)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Member)
                .WithMany(m => m.Orders)
                .HasForeignKey(o => o.MemberId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany() // Product không có navigation property ngược lại
                .HasForeignKey(od => od.ProductId);

            // Member - Feedbacks (1-n)
            modelBuilder.Entity<Feedbacks>()
                .HasOne(f => f.Member)
                .WithMany(m => m.Feedbacks)
                .HasForeignKey(f => f.MemberId);

            // Product - Feedbacks (1-n)
            modelBuilder.Entity<Feedbacks>()
                .HasOne(f => f.Product)
                .WithMany() // Product không có navigation property ngược lại
                .HasForeignKey(f => f.ProductId);

            // Order - Transaction (1-n)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Order)
                .WithMany(o => o.Transactions)
                .HasForeignKey(t => t.OrderId);

            // Cấu hình độ dài và ràng buộc
            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Member>()
                .Property(m => m.Email)
                .HasMaxLength(100)
                .IsRequired();


        }
        // DbSets for your entities
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Feedbacks> Feedbacks { get; set; }
    }
}
