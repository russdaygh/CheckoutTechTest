using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CheckoutTechTest.Models;
using Microsoft.Extensions.Configuration;

namespace CheckoutTechTest.WebApi.Services
{
    public class DynamoAcquiringBank : IAcquiringBank
    {
        public class Settings
        {
            public string TableName { get; set; }
        }

        private readonly IAmazonDynamoDB _dynamo;
        private readonly Settings _settings;

        public DynamoAcquiringBank(IAmazonDynamoDB dynamo, IConfiguration config)
        {
            _dynamo = dynamo;
            _settings = config.GetSection("Payments").Get<Settings>();
        }
        
        public async Task<IPayment> GetPayment(string paymentId)
        {
            var result = await _dynamo.GetItemAsync(_settings.TableName, new Dictionary<string, AttributeValue>{
                {"PaymentId", new AttributeValue{S = paymentId}}
            });

            return result.IsItemSet ? 
                new Payment{
                    PaymentId = result.Item["PaymentId"].S,
                    PaymentRequest = JsonSerializer.Deserialize<PaymentRequest>(result.Item["Data"].S),
                    PaymentStatus = Enum.Parse<PaymentStatus>(result.Item["PaymentStatus"].S)
                }
                : null;
        }

        public async Task<IPayment> SubmitPayment(IPaymentRequest request)
        {
            var paymentId = Guid.NewGuid().ToString();

            await _dynamo.PutItemAsync(_settings.TableName, new Dictionary<string, AttributeValue>{
                {"PaymentId", new AttributeValue{S=paymentId}},
                {"PaymentRequest", new AttributeValue{S=JsonSerializer.Serialize(request)}},
                {"PaymentStatus", new AttributeValue{S=PaymentStatus.Pending.ToString()}}
            });

            return new Payment{
                PaymentId = paymentId,
                PaymentRequest = request,
                PaymentStatus = PaymentStatus.Pending
            };
        }
    }
}