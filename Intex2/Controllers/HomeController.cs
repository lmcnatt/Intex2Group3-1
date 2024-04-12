using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Intex2.Models;
using Intex2.Models.ViewModels;
using NuGet.ProjectModel;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Intex2.Controllers
{
    public class HomeController : Controller
    {
        private IIntex2Repository _repo;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IIntex2Repository repo, UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
            //var modelPath = "Intex2\fraud_onnx_model.onnx"; 
            //var model_session = new InferenceSession(modelPath);
            
            
        }

        public IActionResult Index()
        {
            int id = 0;

            // if (User.Identity.IsAuthenticated)
            // {
            //     var userId = _userManager.GetUserId(User);
            //     // id = _repo.GetHighestRating(userId);
                
            // }
            if (id != 0)
            {
                // Assuming _repo.Recommendations is a DbSet or similar that allows querying
                 var recommendation = _repo.Recommendations.Where(p =>  p.RecId == id).FirstOrDefault();
                var recommendedProducts = new List<Product>();

                // Loop through each recommendation column (rec1 to rec10)
                for (int i = 0; i <= 10; i++)
                {
                    var recId = 0;
                    if (i == 0){recId = recommendation.RecId;}
                    else if (i == 1){recId = recommendation.Rec1;}
                    else if (i == 2){recId = recommendation.Rec2;}
                    else if (i == 3){recId = recommendation.Rec3;}
                    else if (i == 4){recId = recommendation.Rec4;}
                    else if (i == 5){recId = recommendation.Rec5;}
                    else if (i == 6){recId = recommendation.Rec6;}
                    else if (i == 7){recId = recommendation.Rec7;}
                    else if (i == 8){recId = recommendation.Rec8;}
                    else if (i == 9){recId = recommendation.Rec9;}
                    else if (i == 10){recId = recommendation.Rec10;}
                    // Retrieve the product ID for the current recommendation column
                    

                    // Retrieve the product details for the current recommendation ID
                    var product = _repo.Products.FirstOrDefault(p => p.ProductId == recId);

                    // Add the product to the list if it exists
                    if (product != null)
                    {
                        recommendedProducts.Add(product);
                    }
                }


                return View(recommendedProducts);
            }

            else
            {
                int[] topTen = { 23, 19, 21, 22, 20, 12, 18, 15, 11, 13 };
                var recommendedProducts = new List<Product>();

                foreach (var productId in topTen)
                {
                        var product = _repo.Products.FirstOrDefault(p => p.ProductId == productId);
                        if (product != null)
                        {
                            recommendedProducts.Add(product);
                        }
                }

                return View(recommendedProducts);

            }



            // Pass the list of recommended products to the view
            ;
        }

        public IActionResult Products(string? category, string? color, int pageNum = 1)
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


        [Route("ProductDetails/{productId:int}")]
        public IActionResult ProductDetails(int productId)
        {
            var recommendation = _repo.Recommendations.Where(p =>  p.RecId == productId).FirstOrDefault();
            var recommendedProducts = new List<Product>();

                // Loop through each recommendation column (rec1 to rec10)
                for (int i = 0; i <= 10; i++)
                {
                    var recId = 0;
                    if (i == 0){recId = recommendation.RecId;}
                    else if (i == 1){recId = recommendation.Rec1;}
                    else if (i == 2){recId = recommendation.Rec2;}
                    else if (i == 3){recId = recommendation.Rec3;}
                    else if (i == 4){recId = recommendation.Rec4;}
                    else if (i == 5){recId = recommendation.Rec5;}
                    else if (i == 6){recId = recommendation.Rec6;}
                    else if (i == 7){recId = recommendation.Rec7;}
                    else if (i == 8){recId = recommendation.Rec8;}
                    else if (i == 9){recId = recommendation.Rec9;}
                    else if (i == 10){recId = recommendation.Rec10;}
                    // Retrieve the product ID for the current recommendation column
                    

                    // Retrieve the product details for the current recommendation ID
                    var product = _repo.Products.FirstOrDefault(p => p.ProductId == recId);

                    // Add the product to the list if it exists
                    if (product != null)
                    {
                        recommendedProducts.Add(product);
                    }
                }

                return View(recommendedProducts); 
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
