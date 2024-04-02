namespace Intex2.Models
{
    public interface IIntex2Repository
    {
        public IQueryable<Product> Products { get; }
    }
}
