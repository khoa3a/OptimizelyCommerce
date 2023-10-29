using EmptyCommerce1.Models.Pages;
using EmptyCommerce1.Models.ViewModels;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EmptyCommerce1.Controllers
{
    public class HomePageController : PageController<HomePage>
    {
        public IActionResult Index(HomePage currentContent)
        {
            //var viewModel = new HomePageViewModel(currentContent, _contentLoader);
            //var productContent = _contentLoader.Get<ProductContent>(currentContent.Products[0]) as GenericProduct;
            var viewModel = new HomePageViewModel(currentContent);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Test([FromBody] object param)
        {
            return Ok(param);
        }
    }
}
