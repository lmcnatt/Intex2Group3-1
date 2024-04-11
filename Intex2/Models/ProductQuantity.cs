using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    public class ProductQuantity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}