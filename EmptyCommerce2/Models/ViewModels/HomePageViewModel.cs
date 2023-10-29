using EmptyCommerce2.Features.CatalogContent.Product;
using EmptyCommerce2.Models.Media;
using EmptyCommerce2.Models.Pages;
using EPiServer.Cms.Shell;
using EPiServer.Commerce.Catalog.ContentTypes;

namespace EmptyCommerce2.Models.ViewModels
{
    public class HomePageViewModel
    {
        public IList<LaptopProductViewModel>? Products { get; set; } = new List<LaptopProductViewModel>();

        public HomePageViewModel(HomePage currentContent, IContentLoader contentLoader)
        {
            if (currentContent.Products != null)
            {
                foreach (var contentRef in currentContent.Products)
                {
                    var productContent = contentLoader.Get<ProductContent>(contentRef) as LaptopProduct;
                    if (productContent != null)
                    {
                        var product = new LaptopProductViewModel
                        {
                            Price = productContent.Price,
                            Code = productContent.Code,
                            Name = productContent.DisplayName,
                            Image = productContent.ImageFile
                        };

                        Products.Add(product);
                    }
                }
            }
        }
    }
}
