namespace Intex2.Models
{
    public interface IAdminRepository
    {
        IQueryable<Product> Products { get; }
    }
}