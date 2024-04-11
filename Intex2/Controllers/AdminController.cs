using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Intex2.Models;
using Microsoft.EntityFrameworkCore;
using Intex2.Models.ViewModels;

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
        public IActionResult Products(string? category, int pageNum = 1)
        {
            int pageSize = 5;

            var plvm = new ProductsListViewModel
            {
                Products = _repo.Products
                    .Where(x => _repo.ProductCategories
                        .Where(pc => pc.Category.CategoryName == category)
                        .Select(pc => pc.ProductId)
                        .Contains(x.ProductId) || category == null)
                    .OrderBy(x => x.Name)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? _repo.Products.Count() : _repo.Products.Where(x => _repo.ProductCategories
                        .Where(pc => pc.Category.CategoryName == category)
                        .Select(pc => pc.ProductId)
                        .Contains(x.ProductId)).Count()
                },
                CurrentCategory = category
            };

            return View(plvm);
        }

        public IActionResult AddEditProducts()
        {
            return View();
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