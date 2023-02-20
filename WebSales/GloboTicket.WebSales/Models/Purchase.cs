using System.ComponentModel.DataAnnotations;

namespace GloboTicket.WebSales.Models
{
    public class Purchase
    {
        public int Quantity { get; set; }
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }
        [Display(Name = "Name on Card")]
        public string NameOnCard { get; set; }
        [Display(Name = "Verification Code")]
        public int VerificationCode { get; set; }
        [Display(Name = "Expiration Month")]
        public int ExpirationMonth { get; set; }
        [Display(Name = "Expiration Year")]
        public int ExpirationYear { get; set; }
    }
}
