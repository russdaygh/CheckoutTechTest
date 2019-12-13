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
            result.Value.ShouldBe(actualPaymentRequest);
        }
    }
}