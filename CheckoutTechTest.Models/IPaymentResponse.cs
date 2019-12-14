namespace CheckoutTechTest.Models
{
    public interface IPayment
    {
        string PaymentId { get; }

        PaymentStatus PaymentStatus { get; }

        IPaymentRequest PaymentRequest { get; }
    }

    public class Payment : IPayment
    {
        public string PaymentId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public IPaymentRequest PaymentRequest { get; set; }
    }
}