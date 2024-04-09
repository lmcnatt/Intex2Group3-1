using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Intex2.Models;
using Microsoft.EntityFrameworkCore;

namespace Intex2.Controllers
{
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

        public IActionResult Orders()
        {
            return View();
        }
    }
}