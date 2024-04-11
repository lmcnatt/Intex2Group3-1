namespace Intex2.Models.ViewModels
{
    public class EditProductsViewModel
    {
        public required Product Product { get; set; }
        public required IQueryable<Category> Categories { get; set; }
    }
}