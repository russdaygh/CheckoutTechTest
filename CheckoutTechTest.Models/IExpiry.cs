namespace CheckoutTechTest.Models
{
    public interface IExpiry
    {
        int Month { get; }
        int Year { get; }
    }

    public class Expiry : IExpiry
    {
        public int Month { get; set; }

        public int Year { get; set; }
    }
}