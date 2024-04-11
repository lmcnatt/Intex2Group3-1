using Intex2.Components;
using System.ComponentModel.DataAnnotations;

namespace Intex2.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public int NumParts { get; set; }
        public int Price { get; set; }
        public string? ImgLink { get; set; }
        public string? PrimaryColor { get; set; }
        public string? SecondaryColor { get; set; }
        public string? Description { get; set; }
    }
}
