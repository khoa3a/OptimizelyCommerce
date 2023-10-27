using EmptyCommerce2.Models.Pages;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EmptyCommerce2.Controllers
{
    public class HomePageController : PageController<HomePage>
    {
        public IActionResult Index(HomePage currentContent)
        {
            //var viewModel = new HomePageViewModel(currentContent);

            return View(currentContent);
        }
    }
}
