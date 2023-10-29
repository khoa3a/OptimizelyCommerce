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
        GUID = "103f77c1-61bc-41ae-ac1a-a8af0eafee24",
        MetaClassName = "GenericProduct",
        DisplayName = "Generic Product",
        Description = "Generic Product")]
    public class GenericProduct : ProductContent
    {
        [Display(Name = "This is product name", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string? Name { get; set; }

        [Display(Name = "This is product price", GroupName = SystemTabNames.Content, Order = 20)]
        public virtual decimal Price { get; set; }

        [Display(Name = "This is product image", GroupName = SystemTabNames.Content, Order = 30)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference? ImageFile { get; set; }
    }
}
