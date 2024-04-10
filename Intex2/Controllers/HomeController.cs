using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Intex2.Models;
using Intex2.Models.ViewModels;
using NuGet.ProjectModel;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Intex2.Controllers
{
    public class HomeController : Controller
    {
        private IIntex2Repository _repo;

        public HomeController(IIntex2Repository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
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

        [Route("ProductDetails/{productId:int}")]
        public IActionResult ProductDetails(int productId)
        {
            var product = _repo.Products.Where(p =>  p.ProductId == productId).FirstOrDefault();
            return View(product);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public IActionResult Secrets()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
