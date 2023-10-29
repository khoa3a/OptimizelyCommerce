using EmptyCommerce2.Features.Checkout;
using EmptyCommerce2.Features.Extensions;
using EmptyCommerce2.Features.Settings;
using EmptyCommerce2.Models.Pages;
using EmptyCommerce2.Models.Shared;
using EPiServer.Commerce.Order;
using EPiServer.Security;
using EPiServer.Web.Mvc;
using Mediachase.Commerce;
using Mediachase.Commerce.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Specialized;

namespace EmptyCommerce2.Controllers
{
    public class ShoppingCartController : PageController<CartPage>
    {
        private readonly ICartService _cartService;
        private ShoppingCart _cart;
        //private CartWithValidationIssues _wishlist;
        //private CartWithValidationIssues _sharedcart;
        private readonly IOrderRepository _orderRepository;
        //private readonly ICommerceTrackingService _recommendationService;
        //private readonly CartViewModelFactory _cartViewModelFactory;
        private readonly IContentLoader _contentLoader;
        //private readonly IContentRouteHelper _contentRouteHelper;
        //private readonly ReferenceConverter _referenceConverter;
        //private readonly IQuickOrderService _quickOrderService;
        //private readonly ICustomerService _customerService;
        //private readonly ShipmentViewModelFactory _shipmentViewModelFactory;
        private readonly ICheckoutService _checkoutService;
        private readonly IOrderGroupCalculator _orderGroupCalculator;
        //private readonly CartItemViewModelFactory _cartItemViewModelFactory;
        //private readonly IProductService _productService;
        private readonly IContentLanguageAccessor _contentLanguageAccessor;
        private readonly ISettingsService _settingsService;
        private readonly IPaymentService _paymentService;
        private readonly ICurrentMarket _currentMarket;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string b2cMinicart = "/Features/Shared/Views/Header/_HeaderCart.cshtml";

        public ShoppingCartController(
            ICartService cartService,
            IOrderRepository orderRepository,
            //ICommerceTrackingService recommendationService,
            //CartViewModelFactory cartViewModelFactory,
            IContentLoader contentLoader,
            //IContentRouteHelper contentRouteHelper,
            // referenceConverter,
            //IQuickOrderService quickOrderService,
            //ICustomerService customerService,
            //ShipmentViewModelFactory shipmentViewModelFactory,
            ICheckoutService checkoutService,
            IOrderGroupCalculator orderGroupCalculator,
            //CartItemViewModelFactory cartItemViewModelFactory,
            //IProductService productService,
            IContentLanguageAccessor contentLanguageAccessor,
            ISettingsService settingsService,
            IPaymentService paymentService,
            ICurrentMarket currentMarket,
            IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _orderRepository = orderRepository;
            //_recommendationService = recommendationService;
            //_cartViewModelFactory = cartViewModelFactory;
            _contentLoader = contentLoader;
            //_contentRouteHelper = contentRouteHelper;
            //_referenceConverter = referenceConverter;
            //_quickOrderService = quickOrderService;
            //_customerService = customerService;
            //_shipmentViewModelFactory = shipmentViewModelFactory;
            _checkoutService = checkoutService;
            _orderGroupCalculator = orderGroupCalculator;
            //_cartItemViewModelFactory = cartItemViewModelFactory;
            //_productService = productService;
            _contentLanguageAccessor = contentLanguageAccessor;
            _settingsService = settingsService;
            _paymentService = paymentService;
            _currentMarket = currentMarket;
            _httpContextAccessor = httpContextAccessor;
        }

        private ShoppingCart ShoppingCart => _cart ?? (_cart = _cartService.LoadCart(_cartService.DefaultCartName, true));

        //private CartWithValidationIssues WishListWithValidationIssues => _wishlist ?? (_wishlist = _cartService.LoadCart(_cartService.DefaultWishListName, true));

        //private CartWithValidationIssues SharedCardWithValidationIssues => _sharedcart ?? (_sharedcart = _cartService.LoadCart(_cartService.DefaultSharedCartName, true));

        //private CartWithValidationIssues SharedCart => _sharedcart ?? (_sharedcart = _cartService.LoadCart(_cartService.DefaultSharedCartName, OrganizationId, true));

        //private string OrganizationId => _customerService.GetCurrentContact().FoundationOrganization?.OrganizationId.ToString();

        [HttpPost]
        [HttpGet]
        public async Task<ActionResult> Index(CartPage currentPage)
        {
            var messages = string.Empty;
            //if (TempData[Constant.Quote.RequestQuoteStatus] != null)
            //{
            //    var requestQuote = (bool)TempData[Constant.Quote.RequestQuoteStatus];
            //    if (requestQuote)
            //    {
            //        ViewBag.QuoteMessage = "Request quote successfully";
            //    }
            //    else
            //    {
            //        ViewBag.ErrorMessage = "Request quote unsuccessfully";
            //    }
            //}

            //if (CartWithValidationIssues.Cart != null && CartWithValidationIssues.ValidationIssues.Any())
            //{
            //    foreach (var item in CartWithValidationIssues.Cart.GetAllLineItems())
            //    {
            //        messages = GetValidationMessages(item, CartWithValidationIssues.ValidationIssues);
            //    }
            //}

            //var viewModel = _cartViewModelFactory.CreateLargeCartViewModel(ShoppingCart.Cart, currentPage);
            //viewModel.Message = messages;
            //var trackingResponse = await _recommendationService.TrackCart(HttpContext, CartWithValidationIssues.Cart);
            //viewModel.Recommendations = trackingResponse.GetCartRecommendations(_referenceConverter);
            return View(currentPage);
        }

        //[AcceptVerbs(new string[] { "GET", "POST" })]
        //public ActionResult MiniCartDetails()
        //{
        //    var viewModel = _cartViewModelFactory.CreateMiniCartViewModel(CartWithValidationIssues.Cart);
        //    return PartialView(b2cMinicart, viewModel);
        //}

        //public PartialViewResult LoadCartItems()
        //{
        //    var viewModel = _cartViewModelFactory.CreateMiniCartViewModel(CartWithValidationIssues.Cart);
        //    return PartialView("_MiniCartItems", viewModel);
        //}

        //public PartialViewResult LoadMobileCartItems()
        //{
        //    var viewModel = _cartViewModelFactory.CreateMiniCartViewModel(CartWithValidationIssues.Cart);
        //    return PartialView("_MobileMiniCartItems", viewModel);
        //}

        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody] AddToCartRequest param)
        {
            var warningMessage = string.Empty;

            ModelState.Clear();

            if (ShoppingCart.Cart == null)
            {
                _cart = new ShoppingCart
                {
                    Cart = _cartService.LoadOrCreateCart(_cartService.DefaultCartName),
                    ValidationIssues = new Dictionary<ILineItem, List<ValidationIssue>>()
                };
            }

            var result = _cartService.AddToCart(ShoppingCart.Cart, param);

            if (result.IsSuccess)
            {
                _orderRepository.Save(ShoppingCart.Cart);
                //await _recommendationService.TrackCart(HttpContext, CartWithValidationIssues.Cart);


                //return MiniCartDetails();
                return Ok();
            }

            return StatusCode(500, result.ErrorMessage);
        }

        public JsonResult RedirectToCart(string message)
        {
            //var referencePages = _settingsService.GetSiteSettings<ReferencePageSettings>();
            //if (!referencePages?.CartPage.IsNullOrEmpty() ?? false)
            //{
            //    var cartPage = _contentLoader.Get<CartPage>(referencePages.CartPage);
            //    return Json(new { Redirect = cartPage.StaticLinkURL, Message = message });
            //}

            return Json(new { Redirect = Request.Path + Request.QueryString, Message = message });
        }

        [HttpPost]
        public async Task<ActionResult> BuyNow([FromBody] AddToCartRequest param)
        {
            var warningMessage = string.Empty;

            ModelState.Clear();

            if (ShoppingCart.Cart == null)
            {
                _cart = new ShoppingCart
                {
                    Cart = _cartService.LoadOrCreateCart(_cartService.DefaultCartName),
                    ValidationIssues = new Dictionary<ILineItem, List<ValidationIssue>>()
                };
            }

            var result = _cartService.AddToCart(ShoppingCart.Cart, param);
            if (!result.IsSuccess)
            {
                return StatusCode(500, result.ErrorMessage);
            }
            var contact = PrincipalInfo.CurrentPrincipal.GetCustomerContact();
            if (contact == null)
            {
                return RedirectToCart("The contact is invalid");
            }

            var creditCard = contact.ContactCreditCards.FirstOrDefault();
            if (creditCard == null)
            {
                return RedirectToCart("There is not any credit card");
            }

            var shipment = ShoppingCart.Cart.GetFirstShipment();
            if (shipment == null)
            {
                return RedirectToCart("The shopping cart is not exist");
            }

            var shippingAddress = (contact.PreferredShippingAddress ?? contact.ContactAddresses.FirstOrDefault())?.ConvertToOrderAddress(ShoppingCart.Cart);
            if (shippingAddress == null)
            {
                return RedirectToCart("The shipping address is not exist");
            }

            shipment.ShippingAddress = shippingAddress;

            //var shippingMethodViewModels = _shipmentViewModelFactory.CreateShipmentsViewModel(ShoppingCart.Cart).SelectMany(x => x.ShippingMethods);
            //var shippingMethodViewModel = shippingMethodViewModels.Where(x => x.Price != 0)
            //    .OrderBy(x => x.Price)
            //    .FirstOrDefault();

            //If product is virtual set shipping method is Free
            //if (shipment.LineItems.FirstOrDefault().IsVirtualVariant())
            //{
            //    shippingMethodViewModel = shippingMethodViewModels.Where(x => x.Price == 0).FirstOrDefault();
            //}

            //if (shippingMethodViewModel == null)
            //{
            //    return RedirectToCart("The shipping method is invalid");
            //}

            //shipment.ShippingMethodId = shippingMethodViewModel.Id;

            var paymentAddress = (contact.PreferredBillingAddress ?? contact.ContactAddresses.FirstOrDefault())?.ConvertToOrderAddress(ShoppingCart.Cart);
            //if (paymentAddress == null)
            //{
            //    return RedirectToCart("The billing address is not exist");
            //}

            var totals = _orderGroupCalculator.GetOrderGroupTotals(ShoppingCart.Cart);
            var creditCardPayment = _paymentService.GetPaymentMethodsByMarketIdAndLanguageCode(ShoppingCart.Cart.MarketId.Value, _currentMarket.GetCurrentMarket().DefaultLanguage.Name).FirstOrDefault(x => x.SystemKeyword == "GenericCreditCard");
            var payment = ShoppingCart.Cart.CreateCardPayment();

            payment.BillingAddress = paymentAddress;
            payment.CardType = "Credit card";
            payment.PaymentMethodId = creditCardPayment.PaymentMethodId.Value;
            payment.PaymentMethodName = creditCardPayment.SystemKeyword;
            payment.Amount = ShoppingCart.Cart.GetTotal().Amount;
            payment.CreditCardNumber = creditCard.CreditCardNumber;
            payment.CreditCardSecurityCode = creditCard.SecurityCode;
            payment.ExpirationMonth = creditCard.ExpirationMonth ?? 1;
            payment.ExpirationYear = creditCard.ExpirationYear ?? DateTime.Now.Year;
            payment.Status = "Pending";// PaymentStatus.Pending.ToString();
            payment.CustomerName = contact.FullName;
            payment.TransactionType = "Authorization";// TransactionType.Authorization.ToString();
            ShoppingCart.Cart.GetFirstForm().Payments.Add(payment);

            //var issues = _cartService.ValidateCart(ShoppingCart.Cart);
            //if (issues.Keys.Any(x => issues.HasItemBeenRemoved(x)))
            //{
            //    return RedirectToCart("The product is invalid");
            //}
            var order = _checkoutService.PlaceOrder(ShoppingCart.Cart, new ModelStateDictionary());//, new CheckoutViewModel());

            //await _checkoutService.CreateOrUpdateBoughtProductsProfileStore(CartWithValidationIssues.Cart);
            //await _checkoutService.CreateBoughtProductsSegments(CartWithValidationIssues.Cart);
            //await _recommendationService.TrackOrder(HttpContext, order);

            var referencePages = _settingsService.GetSiteSettings<ReferencePageSettings>();
            if (referencePages!=null)
            //if(ShoppingCart.Cart!=null)
            {
                var orderConfirmationPage = _contentLoader.Get<OrderConfirmationPage>(referencePages.OrderConfirmationPage);
                var queryCollection = new NameValueCollection
                {
                    {"contactId", contact.PrimaryKeyId?.ToString()},
                    {"orderNumber", order.OrderLink.OrderGroupId.ToString()}
                };
                var urlRedirect = new UrlBuilder(orderConfirmationPage.StaticLinkURL) { QueryCollection = queryCollection };
                return Json(new { Redirect = urlRedirect.ToString() });
            }

            return RedirectToCart("Something went wrong");
        }
    }
}
