using EPiServer.Commerce.Order;

namespace EmptyCommerce2.Features.Checkout
{
    public class ShoppingCart
    {
        public virtual ICart Cart { get; set; }
        public virtual Dictionary<ILineItem, List<ValidationIssue>> ValidationIssues { get; set; }
    }
}
