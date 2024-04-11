using Microsoft.AspNetCore.Mvc;
using Intex2.Models;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex2.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private IOrderRepository _repo;
        private Cart cart;
        DateTime datetime = DateTime.Now;
        private InferenceSession _session;
        public OrderController(IOrderRepository repoService, Cart cartService,InferenceSession session)
        {
            _repo = repoService;
            cart = cartService;
            _session = session;
        }

        // public Customer Customer { get; set; }
        // public string CustomerId { get; set; }
        // public ICollection<Cart.CartLine> Lines { get; set;}
        // public string Date { get; set; }
        // public string DayOfWeek { get; set; }
        // public string Time { get; set; }
        // public decimal Amount { get; set; }
        // public string CountryOfTransaction { get; set; }
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            // Check ModelState errors
            foreach (var state in ModelState.Values)
            {
                foreach (var error in state.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _repo.GetCustomer(userId);
            order.Customer = customer;
            order.CustomerId = customer.CustomerId;
            order.Lines = cart.Lines.ToArray();
            order.Date = datetime.ToString("dd/MM/yyyy");
            order.DayOfWeek = datetime.ToString("ddd");
            order.Time = datetime.ToString("HH");
            order.Amount = cart.CalculateTotal();
            order.CountryOfTransaction = customer.CountryOfResidence;
    
            float[] data = [float.Parse(order.Time), 
            (float)order.Amount,order.CountryOfTransaction == "United Kingdom" ? 1 : 0,];
        
            int[] dimensions = new int[] {1, 3 };
            
    
            var result = _session.Run(new List<NamedOnnxValue> 
            { 
                NamedOnnxValue.CreateFromTensor("float_input", new DenseTensor<float>(data, dimensions)) 
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = score.First();
            result.Dispose();
            if (prediction == 1)
            {
                order.Fraud = true;
            }
            else
            {
                order.Fraud = false;
            }
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("",
                    "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                _repo.SaveOrder(order);
                cart.Clear();
                if (order.Fraud == false)
                {
                   return RedirectToPage("/Completed", new { orderId = order.OrderID }); 
                }
                else
                {
                    return RedirectToPage("/CompletedFraud", new { orderId = order.OrderID }); 
                }
                
            }
            else
            {
                return View();
            }
        }
    }
}
