using MediatR;

using System.Threading;
using System.Threading.Tasks;

using VehicleTrackingSystem.Application.Commands;
using VehicleTrackingSystem.Application.Managers;

namespace VehicleTrackingSystem.Application.CommandHandlers
{
    public class UserRoleDeleteCommandHandler : IRequestHandler<UserRoleDeleteCommand, bool>
    {

        private readonly IVehicleTrackingUserManager _vehicleTrackingUserManager;

        public UserRoleDeleteCommandHandler(IVehicleTrackingUserManager vehicleTrackingUserManager)
        {
            _vehicleTrackingUserManager = vehicleTrackingUserManager;
        }

        public async Task<bool> Handle(UserRoleDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _vehicleTrackingUserManager.RemoveUserRole(request.UserName, request.RoleName);
        }
    }
}
