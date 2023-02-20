using GloboTicket.Sales.Messages.Purchases;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Sales.Purchasing
{
    class PurchaseTicketHandler
    {
        public PurchaseTicketHandler()
        {
        }

        public async Task Handle(ConsumeContext<PurchaseTicket> context)
        {
            PurchaseTicket message = context.Message;

            var units = message.order.quantity == 1 ? "ticket" : "tickets";
            Console.WriteLine($"Handling purchase for {message.order.quantity} {units}");

            // Consume the offer only once (Idempotent)
            bool offerHasBeenConsumed = await HasOfferBeenConsumed(message.offer.offerGuid);
            if (offerHasBeenConsumed)
            {
                Console.WriteLine("Already consumed");
                return;
            }

            // Validate the request
            if (message.order.quantity > message.offer.maximumQuantity ||
                message.order.quantity < message.offer.minimumQuantity)
            {
                Console.WriteLine("Invalid request");
                throw new InvalidOperationException("Order quantity is out of range");
            }

            // Test invariants
            int quantityRemaining = 3;
            if (message.order.quantity > quantityRemaining)
            {
                Console.WriteLine("Invariant violated");
                await context.Publish(new PurchaseTicketFailed
                {
                    failureReason = FailureReasons.SoldOut,
                    offer = message.offer,
                    order = message.order
                });
            }
        }

        private Task<bool> HasOfferBeenConsumed(Guid offerGuid)
        {
            return Task.FromResult(false);
        }
    }
}