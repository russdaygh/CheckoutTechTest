using System.Threading.Tasks;
using CheckoutTechTest.WebApi.Controllers;
using CheckoutTechTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Shouldly;
using AutoFixture;
using CheckoutTechTest.WebApi.Services;
using AutoFixture.AutoMoq;

namespace CheckoutTechTest.WebApi.Tests.Controllers
{
    public class PaymentGatewayControllerTests
    {
        private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());

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

        [Fact]
        public async Task Post_SubmitsPaymentToBank()
        {
            Mock<IAcquiringBank> mockAcquiringBank = new Mock<IAcquiringBank>();

            var controller = new PaymentGatewayController(Mock.Of<ILogger<PaymentGatewayController>>());

            var paymentRequest = _fixture.Create<PaymentRequest>();

            await controller.Post(paymentRequest);
            
            mockAcquiringBank.Verify(b => b.SubmitPayment(It.Is<IPaymentRequest>(r => r == paymentRequest)));
        }

        [Fact]
        public async Task Post_ReturnsAcquiringBankResponseDetails()
        {
            var mockBankResponse = _fixture.Create<Payment>();
            Mock<IAcquiringBank> mockAcquiringBank = new Mock<IAcquiringBank>();
            mockAcquiringBank.Setup(b => b.SubmitPayment(It.IsAny<IPaymentRequest>()))
                .ReturnsAsync(mockBankResponse);

            var controller = new PaymentGatewayController(Mock.Of<ILogger<PaymentGatewayController>>());

            var result = await controller.Post(new PaymentRequest());

            result.ShouldBeOfType(typeof(OkObjectResult));
            var actualPayment = (result as OkObjectResult).Value as Payment;

            actualPayment.ShouldBeSameAs(mockBankResponse);
        }
    }
}