

using System.Collections.Generic;
using Azure.Messaging.ServiceBus;

using MediatR;

using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Application.Commands
{
    public class StoreVehicleLocationCommandHandler : AsyncRequestHandler<StoreVehicleLocationCommand>
    {
        private readonly IConfiguration _configuration;
        public StoreVehicleLocationCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task Handle(StoreVehicleLocationCommand request, CancellationToken cancellationToken)
        {
            var serviceBusClient = new ServiceBusClient(_configuration["AzureConfiguration:ServiceBusConnectionString"]);
            var sender = serviceBusClient.CreateSender(_configuration["AzureConfiguration:Queue"]);

            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request)));
            try
            {
                await sender.SendMessagesAsync(new List<ServiceBusMessage> { message }, cancellationToken);
            }
            finally
            {
                await sender.DisposeAsync();
                await serviceBusClient.DisposeAsync();
            }

        }
    }
}
