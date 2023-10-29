using EmptyCommerce2.Models.Pages;
using EPiServer.Commerce.Catalog.DataAnnotations;
using Mediachase.Search;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmptyCommerce2.Features.Settings
{
    [SettingsContentType(DisplayName = "Site Structure Settings Page",
        GUID = "3143ddfd-fd90-432c-98fe-f2a153ea0799",
        Description = "Site structure settings",
        SettingsName = "Page references")]
    public class ReferencePageSettings : StandardContentBase
    {
        [CultureSpecific]
        [AllowedTypes(typeof(OrderConfirmationPage))]
        [Display(Name = "Order confirmation page", GroupName = "SiteStructure", Order = 160)]
        public virtual ContentReference OrderConfirmationPage { get; set; }
    }
}
