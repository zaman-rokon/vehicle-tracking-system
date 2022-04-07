using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleTrackingSystem.DataAccess.DBModels
{
    public class Vehicle
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string OwnerName { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}
