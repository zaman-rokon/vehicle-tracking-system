using System.ComponentModel.DataAnnotations;
using System;
using MediatR;
using System.Text.Json.Serialization;

namespace VehicleTrackingSystem.Application.Commands
{
    public class RegisterVehicleCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Model { get; set; }
        public string OwnerName { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
