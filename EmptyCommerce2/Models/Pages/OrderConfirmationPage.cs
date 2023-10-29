using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmptyCommerce2.Models.Pages
{
    [ContentType(
        GroupName = SystemTabNames.Content,
        DisplayName = "This is the OrderConfirmationPage",
        GUID = "6efc49f7-5a3c-4fca-996f-941a9a9243cd"
    )]
    public class OrderConfirmationPage : PageData
    {
        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(Name = "Body text", GroupName = SystemTabNames.Content, Order = 20)]
        public virtual XhtmlString Body { get; set; }
    }
}
