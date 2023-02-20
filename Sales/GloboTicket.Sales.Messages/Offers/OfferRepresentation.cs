using System;

namespace GloboTicket.Sales.Messages.Offers
{
    public class OfferRepresentation
    {
        public Guid offerGuid { get; set; }
        public Guid actGuid { get; set; }
        public Guid venueGuid { get; set; }
        public DateTimeOffset startTime { get; set; }
        public DateTime createdDate { get; set; }

        public decimal unitPrice { get; set; }
        public int minimumQuantity { get; set; }
        public int maximumQuantity { get; set; }
        public DateTime expirationDate { get; set; }

    }
}
