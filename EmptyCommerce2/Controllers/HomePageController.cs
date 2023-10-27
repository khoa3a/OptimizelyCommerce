using EmptyCommerce2.Features.CatalogContent.Product;
using EmptyCommerce2.Models.Pages;
using EmptyCommerce2.Models.ViewModels;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Web.Mvc;
using Mediachase.Commerce.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace EmptyCommerce2.Controllers
{
    public class HomePageController : PageController<HomePage>
    {
        private readonly IContentLoader _contentLoader;

        public HomePageController(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public IActionResult Index(HomePage currentContent)
        {
            var viewModel = new HomePageViewModel(currentContent, _contentLoader);
            //var productContent = _contentLoader.Get<ProductContent>(currentContent.Products[0]) as GenericProduct;

            return View(viewModel);
        }
    }
}
