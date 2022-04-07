using Newtonsoft.Json;

using System;

namespace VehicleTrackingSystem.DataAccess.CosmosRepositories
{
    public class QueryDocument<T>
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }

        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}
