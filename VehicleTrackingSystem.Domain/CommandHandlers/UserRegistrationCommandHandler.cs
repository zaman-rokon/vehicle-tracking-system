using MediatR;

using System.Threading;
using System.Threading.Tasks;

using VehicleTrackingSystem.Application.Commands;
using VehicleTrackingSystem.Application.Managers;

namespace VehicleTrackingSystem.Application.CommandHandlers
{
    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, bool>
    {
        private readonly IVehicleTrackingUserManager _vehicleTrackingUserManager;

        public UserRegistrationCommandHandler(IVehicleTrackingUserManager vehicleTrackingUserManager)
        {
            _vehicleTrackingUserManager = vehicleTrackingUserManager;
        }

        public async Task<bool> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            return await _vehicleTrackingUserManager.RegisterUser(request.Email, request.Password, request.FirstName, request.LastName, request.VehicleRegistrationNumber, request.VehicleModelName);
        }
    }
}
