using System.ComponentModel.DataAnnotations;

using MediatR;

namespace VehicleTrackingSystem.Application.Commands
{
    public class UserRoleDeleteCommand : IRequest<bool>
    {
        public string UserName { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
