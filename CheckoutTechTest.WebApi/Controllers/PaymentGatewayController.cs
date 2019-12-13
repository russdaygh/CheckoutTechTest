using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckoutTechTest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CheckoutTechTest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly ILogger _logger;

        public PaymentGatewayController(ILogger<PaymentGatewayController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Post(IPaymentRequest payment)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            return Ok(payment);
        }
    }
}
