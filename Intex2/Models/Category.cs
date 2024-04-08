namespace Intex2.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<ProductCategory>? ProductCategories { get; set;} = new List<ProductCategory>();
    }
}