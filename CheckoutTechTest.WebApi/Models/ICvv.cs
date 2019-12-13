namespace CheckoutTechTest.WebApi.Models
{
    public interface ICvv
    {
        string Value { get; }
    }

    public class Cvv : ICvv
    {
        public string Value { get; set; }
    }
}