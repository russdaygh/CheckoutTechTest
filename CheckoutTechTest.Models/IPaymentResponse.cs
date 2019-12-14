namespace CheckoutTechTest.Models
{
    public interface IPayment
    {
        string TransactionId { get; }
        PaymentStatus PaymentStatus { get; }

        IPaymentRequest PaymentRequest { get; }
    }

    public class Payment : IPayment
    {
        public string TransactionId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public IPaymentRequest PaymentRequest { get; set; }
    }
}