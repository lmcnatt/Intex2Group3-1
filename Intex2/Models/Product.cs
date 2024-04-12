using Intex2.Components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intex2.Models
{
    public class Product
    {
        [Key][Required] public int ProductId { get; set; }
        [ForeignKey("CategoryId")] public int CategoryId { get; set; }
        public Category Category { get; set; }
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
