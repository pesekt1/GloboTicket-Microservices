using GloboTicket.Sales.Messages.Offers;
using GloboTicket.Sales.Messages.Payments;

namespace GloboTicket.Sales.Messages.Purchases
{
    public class PurchaseTicket
    {
        public OfferRepresentation offer { get; set; }
        public OrderRepresentation order { get; set; }
        public CreditCardRepresentation creditCardPayment { get; set; }
    }
}
