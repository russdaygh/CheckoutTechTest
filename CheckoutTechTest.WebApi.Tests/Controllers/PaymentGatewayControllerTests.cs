namespace CheckoutTechTest.WebApi.Tests.Controllers
{
    [Fixture]
    public class PaymentGatewayControllerTests
    {
        [Fact]
        public async Post_Returns_OkObjectResult()
        {
            var controller = new PaymentGatewayController(Mock.Of<ILogger<PaymentGatewayController>>());

            var result = await controller.Post(new PaymentRequest());

            result.ShouldBeOfType(typeof(OkObjectResult));
        }
    }
}