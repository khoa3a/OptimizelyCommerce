using EPiServer.Commerce.UI.Admin.Payments.Internal;

namespace EmptyCommerce2.Features.Checkout
{
    public interface IPaymentService
    {
        IEnumerable<PaymentMethodViewModel> GetPaymentMethodsByMarketIdAndLanguageCode(string marketId, string languageCode);
    }
}
