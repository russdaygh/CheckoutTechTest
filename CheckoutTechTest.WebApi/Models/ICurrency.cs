namespace CheckoutTechTest.WebApi.Models
{
    public interface ICurrency
    {
        string Value { get; }
    }

    public class Currency : ICurrency
    {
        public string Value { get; set; }
    }
}