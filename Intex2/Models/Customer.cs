using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intex2.Models
{
    public class Customer
    {
        public string CustomerId { get; set; } = "temp";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string CountryOfResidence { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
