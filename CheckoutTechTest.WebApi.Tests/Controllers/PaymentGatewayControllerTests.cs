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

        private readonly PaymentGatewayController _sut;

        private readonly Mock<IAcquiringBank> _mockAcquiringBank = new Mock<IAcquiringBank>();

        public PaymentGatewayControllerTests()
        {
            _mockAcquiringBank = new Mock<IAcquiringBank>();

            _sut = new PaymentGatewayController(Mock.Of<ILogger<PaymentGatewayController>>(),
                _mockAcquiringBank.Object);
        }

        [Fact]
        public async Task Post_Returns_BadRequest_WhenModelInvalid()
        {
            _sut.ModelState.AddModelError("property", "invalid");

            var result = await _sut.Post(new PaymentRequest());

            result.ShouldBeOfType(typeof(BadRequestResult));
        }

        [Fact]
        public async Task Post_SubmitsPaymentToBank()
        {
            var paymentRequest = _fixture.Create<PaymentRequest>();

            await _sut.Post(paymentRequest);
            
            _mockAcquiringBank.Verify(b => b.SubmitPayment(It.Is<IPaymentRequest>(r => r == paymentRequest)));
        }

        [Fact]
        public async Task Post_Returns_AcquiringBankResponseDetails()
        {
            var mockBankResponse = _fixture.Create<Payment>();

            _mockAcquiringBank.Setup(b => b.SubmitPayment(It.IsAny<IPaymentRequest>()))
                .ReturnsAsync(mockBankResponse);

            var result = await _sut.Post(new PaymentRequest());

            result.ShouldBeOfType(typeof(OkObjectResult));
            var actualPayment = (result as OkObjectResult).Value as IPayment;

            actualPayment.ShouldBeSameAs(mockBankResponse);
        }
    }
}