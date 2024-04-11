namespace Intex2.Models
{
    public interface IIntex2Repository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<ProductCategory> ProductCategories { get; }
        public IQueryable<Category> Categories { get; }
        public IQueryable<Customer> Customers { get; }
        void SaveCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        public Customer GetCustomer(string id);
        public IQueryable<Recommendation> Recommendations { get; }
        public Recommendation GetRecommendationById(int id);
        // public int GetHighestRating (string userId);

    }
}
