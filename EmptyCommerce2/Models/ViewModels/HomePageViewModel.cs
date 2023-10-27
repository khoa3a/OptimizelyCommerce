using EmptyCommerce2.Features.CatalogContent.Product;
using EmptyCommerce2.Models.Pages;
using EPiServer.Commerce.Catalog.ContentTypes;

namespace EmptyCommerce2.Models.ViewModels
{
    public class HomePageViewModel
    {
        public IList<GenericProduct>? Products { get; set; } = new List<GenericProduct>();

        public HomePageViewModel(HomePage currentContent, IContentLoader contentLoader)
        {
            foreach (var contentRef in currentContent.Products)
            {
                var productContent = contentLoader.Get<ProductContent>(contentRef) as GenericProduct;
                if (productContent != null)
                {
                    Products.Add(productContent);
                }
            }
        }
    }
}
