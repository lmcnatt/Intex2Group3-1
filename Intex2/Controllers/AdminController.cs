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
        public IActionResult AdminProducts(string? category, int pageNum = 1)
        {
            int pageSize = 5;

            var plvm = new ProductsListViewModel
            {
                Products = _repo.Products
                    .Where(x => x.Category.CategoryName == category)
                    .OrderBy(x => x.Name)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? _repo.Products.Count() : _repo.Products.Where(x => x.Category.CategoryName == category).Count()
                },
                CurrentCategory = category
            };

            return View(plvm);
        }

        [HttpGet]
        public IActionResult AddEditProduct(int productId)
        {
            var eplvm = new EditProductsViewModel
            {
                Product = _repo.Products
                    .Single(p => p.ProductId == productId),

                Categories = _repo.Categories
                    .OrderBy(c => c.CategoryName)
            };

            return View(eplvm);
        }

        public IActionResult Orders(int pageNum = 1)
        {
            int pageSize = 200;

            var totalItems = _repo.Orders.Count();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            var plvm = new OrdersListViewModel
            {
                Orders = _repo.Orders
                    .OrderBy(x => x.OrderID)
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