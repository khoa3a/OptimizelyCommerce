using System.ComponentModel.DataAnnotations;

namespace EmptyCommerce1.Models.Pages
{
    public abstract class AbstractPage : PageData
    {
        [Display(
            Name = "Page Title",
            GroupName = "SEO",
            Order = 10)]
        public virtual string? PageTitle { get; set; }

        [Display(
            Name = "Page Header",
            GroupName = "SEO",
            Order = 20)]
        public virtual string? PageHeader { get; set; }
    }
}
