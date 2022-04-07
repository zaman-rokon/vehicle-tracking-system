namespace VehicleTrackingSystem.ServiceBusTriggerFunction
{
    static class Constants
    {

        public const string StreetNumber = "street_number";
        public const string Route = "route";
        public const string SubLocality = "sublocality";
        public const string Locality = "locality";
        public const string AdministrativeArea = "administrative_area_level_1";
        public const string Country = "country";

    }

    enum Status
    {
        INVALID_REQUEST,
        OK
    }
}
