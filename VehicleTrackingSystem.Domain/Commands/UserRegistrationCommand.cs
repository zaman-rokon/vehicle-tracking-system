using MediatR;

using System.ComponentModel.DataAnnotations;

namespace VehicleTrackingSystem.Application.Commands
{
    public class UserRegistrationCommand : IRequest<bool>
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string VehicleRegistrationNumber { get; set; }

        [Required]
        public string VehicleModelName { get; set; }

    }
}
