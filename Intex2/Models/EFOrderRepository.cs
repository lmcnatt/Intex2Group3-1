using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private IntexDbContext _context;

        public EFOrderRepository(IntexDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public IQueryable<Customer> Customers => _context.Customers;

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));

            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
                foreach (Cart.CartLine cl in order.Lines)
                {
                    _context.CartLine.Add(cl);
                }
                
            }
            _context.SaveChanges();
        }

        public Customer GetCustomer(string id)
        {
            var customer = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();

            return customer;
        }
    }
}
