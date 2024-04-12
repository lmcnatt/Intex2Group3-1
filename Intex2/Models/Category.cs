using System.ComponentModel.DataAnnotations;

namespace Intex2.Models
{
    public class Category
    {
        [Key][Required] public int CategoryId { get; set; }
        [Required] public string CategoryName { get; set; }
    }
}