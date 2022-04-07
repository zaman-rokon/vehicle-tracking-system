using System;

using Newtonsoft.Json.Linq;

namespace VehicleTrackingSystem.DataAccess.DBModels
{
    public class VehicleLocation
    {
        public string UserId { get; set; }
        public string VehicleId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime TrackDateTime { get; set; }
        public dynamic CustomDataJson { get; set; }
        public LocationDetail LocationDetail { get; set; }
    }
}
