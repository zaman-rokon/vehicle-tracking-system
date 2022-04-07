using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;

namespace VehicleTrackingSystem.DataAccess.DBModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
