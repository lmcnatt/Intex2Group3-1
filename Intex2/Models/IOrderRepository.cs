namespace Intex2.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        IQueryable<Customer> Customers { get; }
        void SaveOrder(Order order);
        public Customer GetCustomer(string id);
    }
}
