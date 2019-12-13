using System.Threading.Tasks;
using CheckoutTechTest.WebApi.Controllers;
using CheckoutTechTest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Shouldly;

namespace CheckoutTechTest.WebApi.Tests.Controllers
{
    public class PaymentGatewayControllerTests
    {
        [Fact]
        public async Task Post_Returns_PaymentRequest()
        {
            var controller = new PaymentGatewayController(Mock.Of<ILogger<PaymentGatewayController>>());

            var actualPaymentRequest = new PaymentRequest();

            var result = await controller.Post(actualPaymentRequest);

            result.ShouldBeOfType(typeof(OkObjectResult));
            var objectResult = result as OkObjectResult;
            objectResult.Value.ShouldBe(actualPaymentRequest);
        }

        [Fact]
        public async Task Post_Returns_BadRequest_WhenModelInvalid()
        {
            var controller = new PaymentGatewayController(Mock.Of<ILogger<PaymentGatewayController>>());

            controller.ModelState.AddModelError("property", "invalid");

            var result = await controller.Post(new PaymentRequest());

            result.ShouldBeOfType(typeof(BadRequestResult));
        }
    }
}