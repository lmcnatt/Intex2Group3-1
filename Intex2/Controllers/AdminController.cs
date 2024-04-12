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