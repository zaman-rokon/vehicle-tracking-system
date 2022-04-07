using MediatR;

using System.Threading;
using System.Threading.Tasks;

using VehicleTrackingSystem.Application.Commands;
using VehicleTrackingSystem.Application.Managers;

namespace VehicleTrackingSystem.Application.CommandHandlers
{
    public class UserRoleUpdateCommandHandler : IRequestHandler<UserRoleUpdateCommand, bool>
    {

        private readonly IVehicleTrackingUserManager _vehicleTrackingUserManager;
        public UserRoleUpdateCommandHandler(IVehicleTrackingUserManager vehicleTrackingUserManager)
        {
            _vehicleTrackingUserManager = vehicleTrackingUserManager;
        }

        public async Task<bool> Handle(UserRoleUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _vehicleTrackingUserManager.AssignUserRole(request.UserName, request.RoleName);
        }
    }
}
