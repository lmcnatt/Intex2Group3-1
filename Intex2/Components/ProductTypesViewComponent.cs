using Microsoft.AspNetCore.Mvc;
using Intex2.Models;

namespace Intex2.Components
{
    public class ProductTypesViewComponent : ViewComponent
    {
        private IIntex2Repository _repo;

        public ProductTypesViewComponent(IIntex2Repository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["Category"];

            // Assuming _repo.Categories is of type DbSet<Category>
            var categories = _repo.Categories.Select(c => c.CategoryName).AsEnumerable(); // Adjust this line based on your Category model

            return View(categories);
        }

    }
}
