using System.Threading.Tasks;
using CheckoutTechTest.Models;

namespace CheckoutTechTest.WebApi.Services
{
    public interface IAcquiringBank
    {
        Task<IPayment> SubmitPayment(IPaymentRequest request);
        Task<IPayment> GetPayment(string paymentId);
    }
}