namespace Intex2.Models
{
    public interface IIntex2Repository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<ProductCategory> ProductCategories { get; }
        public IQueryable<Category> Categories { get; }
        void SaveCustomer(Customer customer);
    }
}
