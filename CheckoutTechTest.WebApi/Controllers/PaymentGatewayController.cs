using System.Threading.Tasks;
using CheckoutTechTest.Models;
using CheckoutTechTest.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CheckoutTechTest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly IAcquiringBank _acquiringBank;

        public PaymentGatewayController(ILogger<PaymentGatewayController> logger, IAcquiringBank acquiringBank)
        {
            _logger = logger;
            _acquiringBank = acquiringBank;
        }

        [HttpPost]
        public async Task<ActionResult> Post(IPaymentRequest payment)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            return Ok(await _acquiringBank.SubmitPayment(payment));
        }

        [HttpGet]
        [Route("{paymentId}")]
        public async Task<ActionResult> Get(string paymentId)
        {
            var payment = await _acquiringBank.GetPayment(paymentId);

            return payment != null ? (ActionResult)Ok(payment) : NotFound();
        }
    }
}
