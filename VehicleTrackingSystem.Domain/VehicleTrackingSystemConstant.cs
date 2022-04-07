namespace VehicleTrackingSystem.Application
{
    public static class VehicleTrackingSystemConstant
    {
        public const string AUTHORIZATION_CLAIM_VEHICLE = "vehicle";
        public const string AUTHORIZATION_CLAIM_USERID = "scope";
    }

    public enum ApplicationRole
    {
        ApplicationUser,
        Administrator
    }
}
