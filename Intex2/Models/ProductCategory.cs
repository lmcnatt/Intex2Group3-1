using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set;}
    }
}