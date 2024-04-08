namespace Intex2.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public required IQueryable<Product> Products { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
        public string? CurrentCategory { get; set; }
    }
}
