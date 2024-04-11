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
        // public IQueryable<Customer> CustomerOrders =>
        //  (IQueryable<Customer>)_context.Customers
        //     .Include(c => c.Orders)
        //     .ThenInclude(o => o.Lines)
        //     .Select(c => new Customer{ 
        //         CustomerId = c.CustomerId, 
        //         FirstName = c.FirstName,
        //         LastName = c.LastName,
        //         BirthDate = c.BirthDate,
        //         CountryOfResidence = c.CountryOfResidence,
        //         Gender = c.Gender,
        //         Age = c.Age, 
        //         Orders = (ICollection<Order>)c.Orders.Select(o => new Order
        //                                 {
        //                                     OrderID = o.OrderID,
        //                                     CountryOfTransaction = o.CountryOfTransaction,
        //                                     Date = o.Date,
        //                                     DayOfWeek = o.DayOfWeek,
        //                                     Time = o.Time,
        //                                     EntryMode = o.EntryMode,
        //                                     Amount = o.Amount,
        //                                     TypeOfTransaction = o.TypeOfTransaction,
        //                                     ShippingAddress = o.ShippingAddress,
        //                                     City = o.City,
        //                                     State = o.State,
        //                                     Zip = o.Zip,
        //                                     ShippingCountry = o.ShippingCountry,
        //                                     Bank = o.Bank,
        //                                     TypeOfCard = o.TypeOfCard,
        //                                     Fraud = o.Fraud,
        //                                     Lines = (ICollection<Cart.CartLine>)o.Lines.Select(cl => new Cart.CartLine
        //                                                             {
        //                                                                 Quantity = cl.Quantity,
        //                                                                 Rating = cl.Rating
        //                                                             })

        //                                 })
        //         })
        //     .FirstOrDefault();
        // public int GetMostPurchased(string userId)
        // {
        //     var mostPurchasedProductId = _context.CustomerOrders
        //         .Where(c => c.CustomerId == userId)
        //         .SelectMany(c => c.Orders.SelectMany(o => o.Lines)) // Flatten Orders and Lines
        //         .GroupBy(cl => cl.Product.ProductId) // Group by ProductId
        //         .OrderByDescending(g => g.Sum(cl => cl.Quantity)) // Order by the sum of Quantity
        //         .Select(g => g.Key) // Select the ProductId
        //         .FirstOrDefault(); // Get the first ProductId

        //     return mostPurchasedProductId;
        // }

    }
}
