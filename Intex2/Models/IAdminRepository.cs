namespace Intex2.Models
{
    public interface IAdminRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Product> ProductsWithoutCategory { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<Category> Categories { get; }

        void AddProduct(Product product);
        void EditProduct(Product product);
    }
}