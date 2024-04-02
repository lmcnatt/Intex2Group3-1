using Microsoft.AspNetCore.Mvc;
using Intex2.Models;

namespace Intex2.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repo;
        private Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            _repo = repoService;
            cart = cartService;
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
