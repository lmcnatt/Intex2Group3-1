namespace Intex2.Models
{
    public interface IAdminRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<ProductCategory> ProductCategories { get; }

    }
}