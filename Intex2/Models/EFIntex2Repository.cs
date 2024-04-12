using Intex2.Models;
using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    public class EFIntex2Repository : IIntex2Repository
    {
        private IntexDbContext _context;
        public EFIntex2Repository(IntexDbContext context)
        {
            _context = context;
        }
        public IQueryable<Product> Products => _context.Products.Include(p => p.Category);
        public IQueryable<Category> Categories => _context.Categories;
        public IQueryable<Customer> Customers => _context.Customers;

        public void SaveCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomer(string id)
        {
            var customer = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();

            return customer;
        }

        public IQueryable<Recommendation> Recommendations => _context.Recommendations;

        public Recommendation GetRecommendationById(int id)
        {
            return _context.Recommendations.FirstOrDefault(r => r.RecId == id);
        }
        

        public int? GetMostPurchased (string userId)
        {
            var mostPurchasedProductId = _context.Orders
                .Where(o => o.CustomerId == userId) // Filter orders by userId
                .SelectMany(o => o.Lines) // Flatten the collection of CartLines from all orders
                .GroupBy(cl => cl.Product.ProductId) // Group by ProductId
                .Select(g => new { ProductId = g.Key, TotalQuantity = g.Sum(cl => cl.Quantity) }) // Select ProductId and sum of quantities
                .OrderByDescending(g => g.TotalQuantity) // Order by total quantity in descending order
                .FirstOrDefault()?.ProductId; // Get the ProductId of the most purchased item
                if (mostPurchasedProductId == null)
                {
                    return 0;
                }
                else{
                    return mostPurchasedProductId;
                }

            
        }
        

    }
}
