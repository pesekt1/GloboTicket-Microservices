using GloboTicket.Sales.Messages.Offers;

namespace GloboTicket.Sales.Messages.Purchases
{
    public class PurchaseTicketFailed
    {
        public OfferRepresentation offer { get; set; }
        public OrderRepresentation order { get; set; }
        public string failureReason { get; set; }
    }
}
