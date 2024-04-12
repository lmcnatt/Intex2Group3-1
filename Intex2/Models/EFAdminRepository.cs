using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Intex2.Models
{
    public class EFAdminRepository : IAdminRepository
    {
        private IntexDbContext _context;

        public EFAdminRepository(IntexDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products
            .Include(p => p.Category);
        
        public IQueryable<Product> ProductsWithoutCategory => _context.Products;

        public IQueryable<Order> Orders => _context.Orders;
        public IQueryable<Category> Categories => _context.Categories;
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void EditProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }
    }
}