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
            ViewBag.SelectedProductType = RouteData?.Values["productType"];

            var productTypes = _repo.Products
                .Select(x => x.ProductType)
                .Distinct()
                .OrderBy(x => x);

            return View(productTypes);
        }
    }
}
