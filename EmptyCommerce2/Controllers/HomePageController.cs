using EmptyCommerce2.Features.CatalogContent.Product;
using EmptyCommerce2.Features.Checkout;
using EmptyCommerce2.Features.Settings;
using EmptyCommerce2.Models.Pages;
using EmptyCommerce2.Models.Shared;
using EmptyCommerce2.Models.ViewModels;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Order;
using EPiServer.Security;
using EPiServer.Web.Mvc;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Specialized;

namespace EmptyCommerce2.Controllers
{
    public class HomePageController : PageController<HomePage>
    {
        private readonly IContentLoader _contentLoader;

        public HomePageController(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public IActionResult Index(HomePage currentContent)
        {
            var viewModel = new HomePageViewModel(currentContent, _contentLoader);
            //var productContent = _contentLoader.Get<ProductContent>(currentContent.Products[0]) as GenericProduct;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> BuyNow([FromBody] AddToCartRequest param)
        {
            var warningMessage = string.Empty;

            ModelState.Clear();

            return Ok();

            //if (ShoppingCart.Cart == null)
            //{
            //    _cart = new ShoppingCart
            //    {
            //        Cart = _cartService.LoadOrCreateCart(_cartService.DefaultCartName),
            //        ValidationIssues = new Dictionary<ILineItem, List<ValidationIssue>>()
            //    };
            //}

            //var result = _cartService.AddToCart(ShoppingCart.Cart, param);
            //if (!result.IsSuccess)
            //{
            //    return StatusCode(500, result.ErrorMessage);
            //}
            //var contact = PrincipalInfo.CurrentPrincipal.GetCustomerContact();
            //if (contact == null)
            //{
            //    return RedirectToCart("The contact is invalid");
            //}

            //var creditCard = contact.ContactCreditCards.FirstOrDefault();
            //if (creditCard == null)
            //{
            //    return RedirectToCart("There is not any credit card");
            //}

            //var shipment = ShoppingCart.Cart.GetFirstShipment();
            //if (shipment == null)
            //{
            //    return RedirectToCart("The shopping cart is not exist");
            //}

            //var shippingAddress = (contact.PreferredShippingAddress ?? contact.ContactAddresses.FirstOrDefault())?.ConvertToOrderAddress(ShoppingCart.Cart);
            //if (shippingAddress == null)
            //{
            //    return RedirectToCart("The shipping address is not exist");
            //}

            //shipment.ShippingAddress = shippingAddress;

            ////var shippingMethodViewModels = _shipmentViewModelFactory.CreateShipmentsViewModel(ShoppingCart.Cart).SelectMany(x => x.ShippingMethods);
            ////var shippingMethodViewModel = shippingMethodViewModels.Where(x => x.Price != 0)
            ////    .OrderBy(x => x.Price)
            ////    .FirstOrDefault();

            ////If product is virtual set shipping method is Free
            ////if (shipment.LineItems.FirstOrDefault().IsVirtualVariant())
            ////{
            ////    shippingMethodViewModel = shippingMethodViewModels.Where(x => x.Price == 0).FirstOrDefault();
            ////}

            ////if (shippingMethodViewModel == null)
            ////{
            ////    return RedirectToCart("The shipping method is invalid");
            ////}

            ////shipment.ShippingMethodId = shippingMethodViewModel.Id;

            //var paymentAddress = (contact.PreferredBillingAddress ?? contact.ContactAddresses.FirstOrDefault())?.ConvertToOrderAddress(ShoppingCart.Cart);
            ////if (paymentAddress == null)
            ////{
            ////    return RedirectToCart("The billing address is not exist");
            ////}

            //var totals = _orderGroupCalculator.GetOrderGroupTotals(ShoppingCart.Cart);
            //var creditCardPayment = _paymentService.GetPaymentMethodsByMarketIdAndLanguageCode(ShoppingCart.Cart.MarketId.Value, _currentMarket.GetCurrentMarket().DefaultLanguage.Name).FirstOrDefault(x => x.SystemKeyword == "GenericCreditCard");
            //var payment = ShoppingCart.Cart.CreateCardPayment();

            //payment.BillingAddress = paymentAddress;
            //payment.CardType = "Credit card";
            //payment.PaymentMethodId = creditCardPayment.PaymentMethodId.Value;
            //payment.PaymentMethodName = creditCardPayment.SystemKeyword;
            //payment.Amount = ShoppingCart.Cart.GetTotal().Amount;
            //payment.CreditCardNumber = creditCard.CreditCardNumber;
            //payment.CreditCardSecurityCode = creditCard.SecurityCode;
            //payment.ExpirationMonth = creditCard.ExpirationMonth ?? 1;
            //payment.ExpirationYear = creditCard.ExpirationYear ?? DateTime.Now.Year;
            //payment.Status = "Pending";// PaymentStatus.Pending.ToString();
            //payment.CustomerName = contact.FullName;
            //payment.TransactionType = "Authorization";// TransactionType.Authorization.ToString();
            //ShoppingCart.Cart.GetFirstForm().Payments.Add(payment);

            ////var issues = _cartService.ValidateCart(ShoppingCart.Cart);
            ////if (issues.Keys.Any(x => issues.HasItemBeenRemoved(x)))
            ////{
            ////    return RedirectToCart("The product is invalid");
            ////}
            //var order = _checkoutService.PlaceOrder(ShoppingCart.Cart, new ModelStateDictionary());//, new CheckoutViewModel());

            ////await _checkoutService.CreateOrUpdateBoughtProductsProfileStore(CartWithValidationIssues.Cart);
            ////await _checkoutService.CreateBoughtProductsSegments(CartWithValidationIssues.Cart);
            ////await _recommendationService.TrackOrder(HttpContext, order);

            //var referencePages = _settingsService.GetSiteSettings<ReferencePageSettings>();
            //if (referencePages != null)
            ////if(ShoppingCart.Cart!=null)
            //{
            //    var orderConfirmationPage = _contentLoader.Get<OrderConfirmationPage>(referencePages.OrderConfirmationPage);
            //    var queryCollection = new NameValueCollection
            //    {
            //        {"contactId", contact.PrimaryKeyId?.ToString()},
            //        {"orderNumber", order.OrderLink.OrderGroupId.ToString()}
            //    };
            //    var urlRedirect = new UrlBuilder(orderConfirmationPage.StaticLinkURL) { QueryCollection = queryCollection };
            //    return Json(new { Redirect = urlRedirect.ToString() });
            //}

            //return RedirectToCart("Something went wrong");
        }
    }
}
