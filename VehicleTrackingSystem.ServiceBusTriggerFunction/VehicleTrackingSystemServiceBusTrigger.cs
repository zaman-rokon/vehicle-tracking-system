using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using VehicleTrackingSystem.ServiceBusTriggerFunction.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace VehicleTrackingSystem.ServiceBusTriggerFunction
{
    public static class VehicleTrackingSystemServiceBusTrigger
    {
        [FunctionName("VehicleTrackingSystemServiceBusTrigger")]
        public static async Task RunAsync([ServiceBusTrigger("locationstore", Connection = "ServiceBusConn")] string message,
            [CosmosDB(databaseName: "VTS", collectionName: "LocationStore",
            ConnectionStringSetting = "CosmosDbConnectionString")] IAsyncCollector<dynamic> documentsOut,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var data = JsonConvert.DeserializeObject<StoreVehicleLocation>(message);
            data.CustomDataJson = JObject.Parse(data.CustomData);

            using (var httpClient = new HttpClient())
            {
                var url = string.Format(config["GoogleMapApi"], data.Latitude, data.Longitude);
                var locationData = httpClient.GetStringAsync(new Uri(url)).Result;
                var googleResponse = JsonConvert.DeserializeObject<GoogleResponse>(locationData);
                if (googleResponse.results != null)
                {
                    var addressComponent = googleResponse.results.FirstOrDefault();

                    data.LocationDetail = new LocationDetail()
                    {
                        Latitude = data.Latitude,
                        Longitude = data.Longitude,
                        FormattedAddress = addressComponent?.formatted_address,
                        StreetName = addressComponent?.address_components.Where(it => it.types.Contains(Constants.StreetNumber))?.FirstOrDefault()?.long_name,
                        Route = addressComponent?.address_components.Where(it => it.types.Contains(Constants.Route))?.FirstOrDefault()?.long_name,
                        SubLocality = addressComponent?.address_components.Where(it => it.types.Contains(Constants.SubLocality))?.FirstOrDefault()?.long_name,
                        Locality = addressComponent?.address_components.Where(it => it.types.Contains(Constants.Locality))?.FirstOrDefault()?.long_name,
                        AdministrativeArea = addressComponent?.address_components.Where(it => it.types.Contains(Constants.AdministrativeArea))?.FirstOrDefault()?.long_name,
                        Country = addressComponent?.address_components.Where(it => it.types.Contains(Constants.Country))?.FirstOrDefault()?.long_name,
                    };
                }
            }

            await documentsOut.AddAsync(new
            {
                id = Guid.NewGuid().ToString(),
                data = data,
                partitionKey = $"{data.VehicleId}|Location|{data.TrackDateTime.ToUniversalTime().Date:dd-MM-yyyy}",
                vehicleId = data.VehicleId,
            });
        }
    }
}
