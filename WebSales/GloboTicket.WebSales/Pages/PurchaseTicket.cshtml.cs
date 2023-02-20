using GloboTicket.Sales.Messages.Offers;
using GloboTicket.Sales.Messages.Payments;
using GloboTicket.Sales.Messages.Purchases;
using GloboTicket.WebSales.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace GloboTicket.WebSales.Pages
{
    public class PurchaseTicketModel : PageModel
    {
        private readonly IPublishEndpoint salesEndpoint;

        public PurchaseTicketModel(IPublishEndpoint salesEndpoint)
        {
            this.salesEndpoint = salesEndpoint;
        }

        public IActionResult OnGet()
        {
            Offer = CreateOffer();
            return Page();
        }

        [BindProperty]
        public OfferRepresentation Offer { get; set; }
        [BindProperty]
        public Purchase Purchase { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await salesEndpoint.Publish(new PurchaseTicket
            {
                offer = Offer,
                order = new OrderRepresentation
                {
                    quantity = Purchase.Quantity
                },
                creditCardPayment = new CreditCardRepresentation
                {
                    creditCardNumber = Purchase.CreditCardNumber,
                    cvv = Purchase.VerificationCode,
                    name = Purchase.NameOnCard,
                    expirationMonth = Purchase.ExpirationMonth,
                    expirationYear = Purchase.ExpirationYear
                }
            });

            return RedirectToPage("./Index");
        }

        private static OfferRepresentation CreateOffer()
        {
            return new OfferRepresentation
            {
                offerGuid = Guid.NewGuid(),
                actGuid = Guid.NewGuid(),
                venueGuid = Guid.NewGuid(),
                startTime = new DateTimeOffset(2021, 1, 21, 19, 0, 0, 0, new TimeSpan(-5, 0, 0)),
                createdDate = DateTime.UtcNow,
                expirationDate = DateTime.UtcNow.AddMinutes(30),
                minimumQuantity = 1,
                maximumQuantity = 4,
                unitPrice = 65.00m
            };
        }
    }
}
