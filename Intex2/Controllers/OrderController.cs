using Microsoft.AspNetCore.Mvc;
using Intex2.Models;
using System.Diagnostics.CodeAnalysis;

namespace Intex2.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repo;
        private Cart cart;
        private Customer _customer;
        DateTime datetime = DateTime.Now;

        public OrderController(IOrderRepository repoService, Cart cartService, Customer customer)
        {
            _repo = repoService;
            cart = cartService;
            _customer = customer;
        }
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
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
                order.CountryOfTransaction = _customer.CountryOfResidence;
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
