using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Intex2.Models;
using Microsoft.EntityFrameworkCore;
using Intex2.Models.ViewModels;
using Microsoft.CodeAnalysis.Differencing;

namespace Intex2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IAdminRepository _repo;

        public AdminController(IAdminRepository repoService)
        {
            _repo = repoService;
        }
        public IActionResult AdminProducts(string? category, string? color, int pageNum = 1)
        {
            int pageSize = 5;
            int totalItems = category == null ? _repo.Products.Count() : _repo.Products.Where(x => x.Category.CategoryName == category).Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            if (pageNum < 1)
            {
                pageNum = 1;
            }

            var plvm = new ProductsListViewModel
            {
                Products = _repo.Products
                    .Where(x=> ( category == null || x.Category.CategoryName == category) && 
                    (color == null || x.PrimaryColor == color))
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.PrimaryColor)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPages
                },
                CurrentCategory = category,
                CurrentColor = color,
            };

            return View(plvm);
        }

        
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = _repo.Categories
                .OrderBy(x => x.CategoryName);

            return View("AddEditProduct", new Product());
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            // Check ModelState errors
            foreach (var state in ModelState.Values)
            {
                foreach (var error in state.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                _repo.AddProduct(product);

                return View("Confirmation", product);
            }
            else
            {
                ViewBag.categories = _repo.Categories
                    .OrderBy(x => x.CategoryName);
                
                Console.WriteLine("It didn't work");
                
                return View("AddEditProduct", product);
            }
        }


        [HttpGet]
        public IActionResult EditProduct(int productId)
        {
            var productToEdit = _repo.ProductsWithoutCategory
                .Single(x => x.ProductId == productId);

            ViewBag.Categories = _repo.Categories
                .OrderBy(x => x.CategoryName);

            return View("AddEditProduct", productToEdit);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            _repo.EditProduct(product);

            return RedirectToAction("AdminProducts");
        }

        public IActionResult Orders(string filter, int pageNum = 1)
        {
            int pageSize = 200;

            // Filter orders based on the selected filter value
            var ordersQuery = _repo.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                bool isFraud = filter.ToLower() == "true";
                ordersQuery = ordersQuery.Where(o => o.Fraud == isFraud);
            }

            var totalItems = ordersQuery.Count();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            var plvm = new OrdersListViewModel
            {
                Orders = ordersQuery
                    .OrderBy(x => x.Date)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPages
                }
            };

            return View(plvm);
        }


    }
}