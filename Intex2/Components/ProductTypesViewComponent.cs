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
            ViewBag.SelectedCategory = RouteData?.Values["CategoryName"];

            var categories = _repo.Categories
                .Select(x => x.CategoryName)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }

    }
}
