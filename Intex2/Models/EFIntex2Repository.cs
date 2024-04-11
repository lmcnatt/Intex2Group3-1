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
        public IQueryable<Product> Products => _context.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                                                                .Select(p => new Product
                                                                {
                                                                    ProductId = p.ProductId,
                                                                    Name = p.Name,
                                                                    Year = p.Year,
                                                                    NumParts = p.NumParts,
                                                                    Price = p.Price,
                                                                    ImgLink = p.ImgLink,
                                                                    PrimaryColor = p.PrimaryColor,
                                                                    SecondaryColor = p.SecondaryColor,
                                                                    Description = p.Description,
                                                                    ProductCategories = (ICollection<ProductCategory>)p.ProductCategories.Select(pc => new ProductCategory
                                                                    {
                                                                        Category = pc.Category,
                                                                        CategoryId = pc.CategoryId
                                                                    })
                                                                });

        public IQueryable<ProductCategory> ProductCategories => _context.ProductCategories;
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
        public int GetHighestRating (string userId)
        {
            int productId = 0;
            var result = _context.Set<ProductQuantity>()
                .FromSqlInterpolated($@"SELECT CL.ProductID, SUM(CL.Quantity) as Quantity
                FROM Customers C
                JOIN Orders O ON O.CustomerId = C.CustomerId
                JOIN CartLine CL ON CL.OrderId = O.OrderId
                WHERE C.CustomerId = {userId}
                GROUP BY CL.ProductId
                ORDER BY SUM(CL.Quantity) DESC
                LIMIT 1").FirstOrDefault();
            if (result != null)
            {
                productId = result.ProductId;
            }
            return productId;
        }

    }
}