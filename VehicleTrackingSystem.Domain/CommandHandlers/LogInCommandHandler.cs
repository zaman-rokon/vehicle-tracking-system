using MediatR;

using System.Threading;
using System.Threading.Tasks;

using VehicleTrackingSystem.Application.Commands;
using VehicleTrackingSystem.Application.Managers;
using VehicleTrackingSystem.Application.ResponseModels;

namespace VehicleTrackingSystem.Application.CommandHandlers
{
    public class LogInCommandHandler : IRequestHandler<LogInCommand, TokenResponse>
    {
        private readonly IVehicleTrackingUserManager _vehicleTrackingUserManager;

        public LogInCommandHandler(IVehicleTrackingUserManager vehicleTrackingUserManager)
        {
            _vehicleTrackingUserManager = vehicleTrackingUserManager;
        }

        public async Task<TokenResponse> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            return await _vehicleTrackingUserManager.GetUserToken(request.Email, request.Password);
        }
    }
}
