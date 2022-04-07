using AutoMapper;

using MediatR;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using VehicleTrackingSystem.Application.Commands;
using VehicleTrackingSystem.DataAccess.DBModels;
using VehicleTrackingSystem.DataAccess.UnitOfWork;

namespace VehicleTrackingSystem.Application.CommandHandlers
{
    public class RegisterVehicleCommandHandler : IRequestHandler<RegisterVehicleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterVehicleCommandHandler(IMapper mapper
            , IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<bool> Handle(RegisterVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = _mapper.Map<Vehicle>(request);
            var user = _unitOfWork.ApplicationUsers.Get(request.UserId);
            bool result = false;

            if (vehicle != null && user != null)
            {
                vehicle.Id = Guid.NewGuid().ToString();
                user.Vehicles = new List<Vehicle> { vehicle };
                _unitOfWork.Complete();
                result = true;
            }
            return Task.FromResult(result);
        }
    }
}
