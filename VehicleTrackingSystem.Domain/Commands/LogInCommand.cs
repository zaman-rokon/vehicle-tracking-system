using MediatR;

using System.ComponentModel.DataAnnotations;

using VehicleTrackingSystem.Application.ResponseModels;

namespace VehicleTrackingSystem.Application.Commands
{
    public class LogInCommand : IRequest<TokenResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
