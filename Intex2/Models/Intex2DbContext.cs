using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    public class IntexDbContext : IdentityDbContext
    {
        public IntexDbContext(DbContextOptions<IntexDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}