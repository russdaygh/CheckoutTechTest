using System.ComponentModel.DataAnnotations;

namespace CheckoutTechTest.WebApi.Models
{
    public interface IPaymentRequest
    {
        ICardNumber CardNumber { get; }

        IExpiry Expiry { get; }

        decimal Amount { get; }

        ICurrency Currency { get; }

        ICvv Cvv { get; }
    }

    public class PaymentRequest : IPaymentRequest
    {
        [Required]
        public ICardNumber CardNumber { get; set; }

        [Required]
        public IExpiry Expiry { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public ICurrency Currency { get; set; }

        [Required]
        public ICvv Cvv { get; set; }
    }
}