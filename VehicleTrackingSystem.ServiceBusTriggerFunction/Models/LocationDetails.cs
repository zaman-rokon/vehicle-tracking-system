namespace VehicleTrackingSystem.ServiceBusTriggerFunction.Models
{
    public class LocationDetail
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string FormattedAddress { get; set; }
        public string StreetName { get; set; }
        public string Route { get; set; }
        public string SubLocality { get; set; }
        public string Locality { get; set; }
        public string AdministrativeArea { get; set; }
        public string Country { get; set; }
    }
}
