using EPiServer.Commerce.Catalog.ContentTypes;
using System.ComponentModel.DataAnnotations;

namespace EmptyCommerce2.Models.Pages
{
    [ContentType(
        GroupName = SystemTabNames.Content,
        DisplayName = "This is the HomePage",
        GUID = "252dd632-a2f1-4068-8f9f-04f822548b68"
    )]
    public class HomePage : PageData
    {
        [Display(Name = "Products",
            Description = "Products associated with this content.",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [AllowedTypes(AllowedTypes = new[] { typeof(ProductContent) })]
        public virtual IList<ContentReference> Products { get; set; }

        //[Display(
        //    Name = "Product Area",
        //    GroupName = SystemTabNames.Content,
        //    Order = 20)]
        //[AllowedTypes(AllowedTypes = new[] { typeof(ProductContent) })]
        //public virtual ContentArea? ProductArea { get; set; }
    }
}
