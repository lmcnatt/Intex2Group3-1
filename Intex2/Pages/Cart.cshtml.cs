using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Intex2.Infrastructure;
using Intex2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;

namespace Intex2.Pages 
{
    [Authorize(Roles = "Customer")]
    public class CartModel : PageModel
    {
        private IIntex2Repository _repo;

        public Cart Cart { get; set; }
        public CartModel(IIntex2Repository temp, Cart cartService)
        {
            _repo = temp;
            Cart = cartService;
        }


        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int productId, string color, string category, int pageNum)
        {
            Product? product = _repo.Products
              .FirstOrDefault(x => x.ProductId == productId);

            if (product != null)
            {
                Cart.AddItem(product, 1);
            }
            
            return RedirectToAction( "Products", "Home", new {
                category = category,
                color = color,
                pageNum = pageNum
            });
        }


        public IActionResult OnPostRemove(int productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(x => x.Product.ProductId == productId).Product);

            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
