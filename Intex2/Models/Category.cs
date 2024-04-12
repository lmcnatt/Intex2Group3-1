using System.ComponentModel.DataAnnotations;

namespace Intex2.Models
{
    public class Category
    {
        [Key][Required] public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}