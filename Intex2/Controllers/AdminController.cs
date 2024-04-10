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

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult AddEditProducts()
        {
            return View();
        }

        public IActionResult Orders(int pageNum = 1)
        {
            int pageSize = 100;

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
                    TotalItems = _repo.Orders.Count()
                }
            };

            return View(plvm);
        }
    }
}