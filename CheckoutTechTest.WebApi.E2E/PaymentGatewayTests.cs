using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using CheckoutTechTest.Models;
using Polly;
using Xunit;

namespace CheckoutTechTest.WebApi.E2E
{
    public class PaymentGatewayTests
    {
        private const string Hostname = "localhost";

        [Fact]
        public async Task CanPostAndRetrievePayment()
        {
            var mockPaymentRequest = new Fixture().Customize(new AutoMoqCustomization()).Create<IPaymentRequest>();

            var httpClient = new HttpClient();

            await Policy.Handle<Exception>().WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(5))
                .ExecuteAsync(async () =>
                {
                    var response = await httpClient.GetAsync($"http://{Hostname}:8080/healthcheck");
                    response.EnsureSuccessStatusCode();
                });

            var response = await httpClient.PostAsync($"http://{Hostname}:8080/paymentgateway",
                new StringContent(JsonSerializer.Serialize(mockPaymentRequest),
                Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var payment = JsonSerializer.Deserialize<Payment>(await response.Content.ReadAsStringAsync());

            var getResponse = await httpClient.GetStringAsync($"http://{Hostname}:8080/paymentgateway/{payment.PaymentId}");

            var storedPayment = JsonSerializer.Deserialize<Payment>(getResponse);

            Assert.Equal(mockPaymentRequest, storedPayment.PaymentRequest);
        }
    }
}
