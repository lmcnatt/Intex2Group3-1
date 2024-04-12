using Microsoft.AspNetCore.Mvc;
using Intex2.Models;

namespace Intex2.Components
{
    public class AdminProductTypesViewComponent : ViewComponent
    {
        private IIntex2Repository _repo;

        public AdminProductTypesViewComponent(IIntex2Repository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["Category"];

            // Assuming _repo.Categories is of type DbSet<Category>
            var categories = _repo.Categories.
            Select(c => c.CategoryName)
            .Distinct()
            .AsEnumerable(); // Adjust this line based on your Category model

            
            return View(categories);
        }

    }
}
