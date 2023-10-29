using EPiServer.Commerce.Order;
using EPiServer.Framework.Localization;
using Mediachase.Commerce.Orders.Exceptions;
using Mediachase.Commerce.Orders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection.Metadata;

namespace EmptyCommerce2.Features.Checkout
{
    public class CheckoutService:ICheckoutService
    {
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderGroupCalculator _orderGroupCalculator;

        public CheckoutService(
            IPaymentProcessor paymentProcessor, 
            IOrderRepository orderRepository,
            IOrderGroupCalculator orderGroupCalculator)
        {
            _paymentProcessor = paymentProcessor;
            _orderRepository = orderRepository;
            _orderGroupCalculator = orderGroupCalculator;
        }

        public IPurchaseOrder PlaceOrder(ICart cart, ModelStateDictionary modelState)
        {
            try
            {               

                var processPayments = cart.ProcessPayments(_paymentProcessor, _orderGroupCalculator);
                var unsuccessPayments = processPayments.Where(x => !x.IsSuccessful);
                if (unsuccessPayments != null && unsuccessPayments.Any())
                {
                    throw new InvalidOperationException(string.Join("\n", unsuccessPayments.Select(x => x.Message)));
                }

                var processedPayments = cart.GetFirstForm().Payments.Where(x => x.Status.Equals(PaymentStatus.Processed.ToString()));

                if (!processedPayments.Any())
                {
                    // Return null in case there is no payment was processed.
                    return null;
                }

                var totalProcessedAmount = processedPayments.Sum(x => x.Amount);
                if (totalProcessedAmount != cart.GetTotal(_orderGroupCalculator).Amount)
                {
                    throw new InvalidOperationException("Wrong amount");
                }

                var orderReference = _orderRepository.SaveAsPurchaseOrder(cart);
                var purchaseOrder = _orderRepository.Load<IPurchaseOrder>(orderReference.OrderGroupId);
                _orderRepository.Delete(cart.OrderLink);

                cart.AdjustInventoryOrRemoveLineItems((item, validationIssue) => { });                

                return purchaseOrder;
            }
            catch (PaymentException ex)
            {
                modelState.AddModelError("", "ProcessingPaymentFailure" + ex.Message);
            }
            catch (Exception ex)
            {
                modelState.AddModelError("", ex.Message);
            }

            return null;
        }
    }
}
