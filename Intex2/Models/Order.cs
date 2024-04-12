using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Intex2.Models.Cart;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex2.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();
        public Customer? Customer { get; set; }
        public string? CustomerId { get; set; }
        public string? CountryOfTransaction { get; set; }
        public string? Date { get; set; }
        public string? DayOfWeek { get; set; }
        public string? Time { get; set; }
        public string EntryMode { get; set; } = "CVC";
        public int Amount { get; set; }
        public string TypeOfTransaction { get; set; } = "Online";

        [Required(ErrorMessage = "Please enter the first address line")]
        public string? ShippingAddress { get; set; }


        [Required(ErrorMessage = "Please enter a city name")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        public string? State { get; set; }
        [Required(ErrorMessage = "Please enter a zip code")]
        public string? Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public string? ShippingCountry { get; set; }
        [Required(ErrorMessage = "Please enter a bank name")]
        public string? Bank { get; set; }
        [Required(ErrorMessage = "Please enter your type of card")]
        public string? TypeOfCard { get; set; }

        public bool Fraud { get; set; } = false;

       
    }
}
