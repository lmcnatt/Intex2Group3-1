using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    public class IntexDbContext : IdentityDbContext
    {
        public IntexDbContext(DbContextOptions<IntexDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Cart.CartLine> CartLine { get; set;}
        public DbSet<Customer> CustomerOrders {get; set;}

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);

        //     modelBuilder.Entity<Customer>()
        //         .HasOne(c => c.User)
        //         .WithOne()
        //         .HasForeignKey<Customer>(c => c.CustomerId)
        //         .OnDelete(DeleteBehavior.Cascade);
        // }
    }
}