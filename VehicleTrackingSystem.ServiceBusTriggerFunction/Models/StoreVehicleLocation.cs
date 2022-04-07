using System;

using Newtonsoft.Json.Linq;

namespace VehicleTrackingSystem.ServiceBusTriggerFunction.Models
{
    public class StoreVehicleLocation
    {
        public string UserId { get; set; }
        public string VehicleId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime TrackDateTime { get; set; }
        public string CustomData { get; set; }
        public JObject CustomDataJson { get; set; }
        public LocationDetail LocationDetail { get; set; } = new LocationDetail();
    }
}
