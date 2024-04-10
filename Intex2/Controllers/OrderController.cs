﻿using Microsoft.AspNetCore.Mvc;
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

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
        public ICollection<Cart.CartLine> Lines { get; set;}
        public string Date { get; set; }
        public string DayOfWeek { get; set; }
        public string Time { get; set; }
        public decimal Amount { get; set; }
        public string CountryOfTransaction { get; set; }
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

            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("",
                    "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                
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
