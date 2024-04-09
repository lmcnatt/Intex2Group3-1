using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Intex2.Infrastructure;
using Intex2.Models;

namespace Intex2.Pages 
{
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

        public PartialViewResult OnPost(int productId)
        {
            Product product = _repo.Products.FirstOrDefault(x => x.ProductId == productId);

            if (product != null)
            {
                Cart.AddItem(product, 1);
                int cartCount = Cart.Lines.Sum(x => x.Quantity);
                return Partial("_CartCountPartial", cartCount);
            }

            return Partial("_CartCountPartial", 0);
        }


        public IActionResult OnPostRemove(int productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(x => x.Product.ProductId == productId).Product);

            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
