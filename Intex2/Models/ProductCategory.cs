using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    [PrimaryKey(nameof(ProductId), nameof(CategoryId))]
    public class ProductCategory
    {
        public virtual Product? Product { get; set; }
        public int ProductId { get; set; }
        public virtual Category? Category { get; set; }
        public int CategoryId { get; set;}
    }
}