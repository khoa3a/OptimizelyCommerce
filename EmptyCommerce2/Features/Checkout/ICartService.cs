using EmptyCommerce2.Models.Shared;
using EPiServer.Commerce.Order;

namespace EmptyCommerce2.Features.Checkout
{
    public interface ICartService
    {
        ExecutionResult AddToCart(ICart cart, AddToCartRequest request);
        ShoppingCart LoadCart(string name, bool validate);
        ShoppingCart LoadCart(string name, string contactId, bool validate);
        ICart LoadOrCreateCart(string name);
        ICart LoadOrCreateCart(string name, string contactId);
        Dictionary<ILineItem, List<ValidationIssue>> ValidateCart(ICart cart);
        string DefaultCartName { get; }
    }
}
