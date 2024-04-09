using Microsoft.AspNetCore.Mvc;
using Intex2.Models;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Intex2.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private IOrderRepository _repo;
        private Cart cart;
        DateTime datetime = DateTime.Now;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            _repo = repoService;
            cart = cartService;
        }
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _repo.GetCustomer(userId);

            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("",
                    "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                order.Date = datetime.ToString("dd/MM/yyyy");
                order.DayOfWeek = datetime.ToString("ddd");
                order.Time = datetime.ToString("H");
                order.Amount = cart.CalculateTotal();
                order.CountryOfTransaction = customer.CountryOfResidence;
                _repo.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }
            else
            {
                return View();
            }
        }
    }
}
