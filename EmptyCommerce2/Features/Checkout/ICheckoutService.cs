using EPiServer.Commerce.Order;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmptyCommerce2.Features.Checkout
{
    public interface ICheckoutService
    {
        IPurchaseOrder PlaceOrder(ICart cart, ModelStateDictionary modelState);
    }
}
