using EmptyCommerce2.Models.Shared;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Commerce.Marketing;
using EPiServer.Commerce.Order;
using EPiServer.Web;
using EPiServer.Security;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Inventory;
using Mediachase.Commerce.Security;

namespace EmptyCommerce2.Features.Checkout
{
    public class CartService : ICartService
    {
        private readonly ReferenceConverter _referenceConverter;
        private readonly IContentLoader _contentLoader;
        private readonly IOrderGroupFactory _orderGroupFactory;
        private readonly IOrderRepository _orderRepository;
        private readonly CustomerContext _customerContext;
        //private readonly ICurrencyService _currencyService;
        private readonly ICurrentMarket _currentMarket;

        public string DefaultCartName => "Default" + SiteDefinition.Current.StartPage.ID;

        public CartService(
            ReferenceConverter referenceConverter,
            IContentLoader contentLoader,
            IOrderGroupFactory orderGroupFactory,
            IOrderRepository orderRepository,
            ICurrentMarket currentMarket
            )
        {
            _referenceConverter = referenceConverter;
            _contentLoader = contentLoader;
            _orderGroupFactory = orderGroupFactory;
            _orderRepository = orderRepository;
            _currentMarket = currentMarket;
            _customerContext = CustomerContext.Current;
        }

        public ExecutionResult AddToCart(ICart cart, AddToCartRequest request)
        {
            var contentLink = _referenceConverter.GetContentLink(request.Code);
            var entryContent = _contentLoader.Get<EntryContentBase>(contentLink);
            return AddToCart(cart, entryContent, 1);
        }

        public ExecutionResult AddToCart(ICart cart, EntryContentBase entryContent, decimal quantity)
        {
            var result = new ExecutionResult();
            var contact = PrincipalInfo.CurrentPrincipal.GetCustomerContact();

            if (contact?.OwnerId != null)
            {
                //var org = cart.GetString("OwnerOrg");
                //if (string.IsNullOrEmpty(org))
                //{
                //    cart.Properties["OwnerOrg"] = contact.OwnerId.Value.ToString().ToLower();
                //}

                cart.Properties["OwnerOrg"] = contact.OwnerId.Value.ToString().ToLower();
            }

            //IWarehouse warehouse = null;

            var form = cart.GetFirstForm();
            if (form == null)
            {
                form = _orderGroupFactory.CreateOrderForm(cart);
                form.Name = cart.Name;
                cart.Forms.Add(form);
            }

            var shipment = cart.GetFirstForm().Shipments.FirstOrDefault();

            if (shipment == null)
            {
                var cartFirstShipment = cart.GetFirstShipment();
                if (cartFirstShipment == null)
                {
                    shipment = _orderGroupFactory.CreateShipment(cart);
                    cart.GetFirstForm().Shipments.Add(shipment);
                }
                else
                {
                    if (cartFirstShipment.LineItems.Count > 0)
                    {
                        shipment = _orderGroupFactory.CreateShipment(cart);
                        cart.GetFirstForm().Shipments.Add(shipment);
                    }
                    else
                    {
                        shipment = cartFirstShipment;
                    }
                }
            }

            var lineItem = shipment.LineItems.FirstOrDefault(x => x.Code == entryContent.Code);
            decimal originalLineItemQuantity = 0;

            if (lineItem == null)
            {
                lineItem = cart.CreateLineItem(entryContent.Code, _orderGroupFactory);
                var lineDisplayName = entryContent.DisplayName;

                lineItem.DisplayName = lineDisplayName;
                lineItem.Quantity = quantity;
                cart.AddLineItem(shipment, lineItem);
            }
            else
            {
                originalLineItemQuantity = lineItem.Quantity;
                cart.UpdateLineItemQuantity(shipment, lineItem, lineItem.Quantity + quantity);
            }

            //var validationIssues = ValidateCart(cart);
            var newLineItem = shipment.LineItems.FirstOrDefault(x => x.Code == entryContent.Code);
            var isAdded = (newLineItem != null ? newLineItem.Quantity : 0) - originalLineItemQuantity > 0;

            //AddValidationMessagesToResult(result, lineItem, validationIssues, isAdded);

            return result;
        }

        public ShoppingCart LoadCart(string name, bool validate) => LoadCart(name, _customerContext.CurrentContactId.ToString(), validate);

        public ShoppingCart LoadCart(string name, string contactId, bool validate)
        {
            var validationIssues = new Dictionary<ILineItem, List<ValidationIssue>>();
            var cart = !string.IsNullOrEmpty(contactId) ? _orderRepository.LoadOrCreateCart<ICart>(new Guid(contactId), name, _currentMarket) : null;
            if (cart != null)
            {
                //SetCartCurrency(cart, _currencyService.GetCurrentCurrency());
                if (validate)
                {
                    validationIssues = ValidateCart(cart);
                    if (validationIssues.Any())
                    {
                        _orderRepository.Save(cart);
                    }
                }
            }

            return new ShoppingCart
            {
                Cart = cart,
                ValidationIssues = validationIssues
            };
        }

        public Dictionary<ILineItem, List<ValidationIssue>> ValidateCart(ICart cart)
        {
            var validationIssues = new Dictionary<ILineItem, List<ValidationIssue>>();         

            //TODO

            return validationIssues;
        }

        public ICart LoadOrCreateCart(string name) => LoadOrCreateCart(name, _customerContext.CurrentContactId.ToString());

        public ICart LoadOrCreateCart(string name, string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                return null;
            }
            else
            {
                var cart = _orderRepository.LoadOrCreateCart<ICart>(new Guid(contactId), name, _currentMarket);
                //if (cart != null)
                //{
                //    SetCartCurrency(cart, _currencyService.GetCurrentCurrency());
                //}

                return cart;
            }
        }
    }
}
