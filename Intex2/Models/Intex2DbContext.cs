using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    public class IntexDbContext : IdentityDbContext
    {
        public IntexDbContext(DbContextOptions<IntexDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Cart.CartLine> CartLine { get; set;}
        // public DbSet<Customer> CustomerOrders {get; set;}
    }
}