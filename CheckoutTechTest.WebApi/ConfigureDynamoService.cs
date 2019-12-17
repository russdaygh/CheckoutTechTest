using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace CheckoutTechTest.WebApi
{
    public class ConfigureDynamoService : IHostedService
    {
        private readonly IAmazonDynamoDB _dynamo;
        private readonly ILogger _logger;

        public ConfigureDynamoService(IAmazonDynamoDB dynamo, ILogger<ConfigureDynamoService> logger)
        {
            _dynamo = dynamo;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Policy.Handle<Exception>(exception =>
            {
                _logger.LogError(exception, "Failed to create Dynamo table");
                return true;
            }).WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(5)).ExecuteAsync(async () => {
                _logger.LogInformation("Creating Dynamo table");
                return await _dynamo.CreateTableAsync("Payments",
                    new List<KeySchemaElement>{
                        new KeySchemaElement{AttributeName="PaymentId", KeyType = KeyType.HASH}
                    },
                    new List<AttributeDefinition>{
                        new AttributeDefinition{AttributeName="PaymentId", AttributeType = ScalarAttributeType.S}
                    },
                    new ProvisionedThroughput { ReadCapacityUnits = 5, WriteCapacityUnits = 5 },
                    cancellationToken);
            });
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}