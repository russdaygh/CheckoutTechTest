namespace CheckoutTechTest.WebApi.Models
{
    public interface ICardNumber
    {
        string Value { get; }
    }

    public class CardNumber : ICardNumber
    {
        public string Value { get; set; }
    }
}