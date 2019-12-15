using System.Threading.Tasks;
using CheckoutTechTest.Models;

namespace CheckoutTechTest.WebApi.Services
{
    public class DynamoAcquiringBank : IAcquiringBank
    {
        public Task<IPayment> GetPayment(string paymentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IPayment> SubmitPayment(IPaymentRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}