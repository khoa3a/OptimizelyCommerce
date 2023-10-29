using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using Mediachase.Commerce.Orders;
using Mediachase.Search;
using System.ComponentModel.DataAnnotations;
using System;
using EPiServer.Web;

namespace EmptyCommerce2.Features.CatalogContent.Product
{
    [CatalogContentType(
        GUID = "448d272b-0ba2-4394-9253-82678f80517a",
        MetaClassName = "LaptoProduct",
        DisplayName = "Laptop Product",
        Description = "Laptop Product")]
    public class LaptopProduct : ProductContent
    {
        [Display(Name = "This is Laptop name", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string? Name { get; set; }

        [Display(Name = "This is Laptop price", GroupName = SystemTabNames.Content, Order = 20)]
        public virtual decimal Price { get; set; }

        [Display(Name = "This is Laptop image", GroupName = SystemTabNames.Content, Order = 30)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference? ImageFile { get; set; }
    }
}
