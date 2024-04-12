using Microsoft.AspNetCore.Mvc;
using Intex2.Models;

namespace Intex2.Components
{
    public class AdminProductColorViewComponent : ViewComponent
    {
        private IIntex2Repository _repo;

        public AdminProductColorViewComponent(IIntex2Repository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedColor = RouteData?.Values["PrimaryColor"];

            // Assuming _repo.Categories is of type DbSet<Category>
            var colors = _repo.Products.
            Select(c => c.PrimaryColor)
            .Distinct()
            .AsEnumerable(); // Adjust this line based on your Category model

            
            return View(colors);
        }

    }
}
