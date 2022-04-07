using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleTrackingSystem.Application.ResponseModels;
using VehicleTrackingSystem.DataAccess.CosmosRepositories;
using VehicleTrackingSystem.DataAccess.DbContext;
using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.Application.Managers
{
    public class VehicleManager : IVehicleManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IVehicleLocationRepository _vehicleLocationRepository;
        private readonly IMapper _mapper;
        public VehicleManager(IMapper mapper
            , ApplicationDbContext applicationDbContext
            , IVehicleLocationRepository vehicleLocationRepository)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _vehicleLocationRepository = vehicleLocationRepository;
        }

        public async Task<IEnumerable<VehicleLocation>> GetVehicleLocationsAsync(string vehicleId, DateTime startDateTime, DateTime endDateTime)
        {
            var data = await _vehicleLocationRepository.GetAllAsync(vehicleId, startDateTime, endDateTime);
            return data;
        }

        public async Task<VehicleLocation> GetVehicleLastLocationAsync(string vehicleId, DateTime locationDate)
        {
            var data = await _vehicleLocationRepository.GetLastLocationAsync(vehicleId, locationDate);
            return data;
        }

        public List<VehicleResponse> GetVehiclesByUserId(string id)
        {
            var result = _applicationDbContext.Vehicles.Include(nameof(ApplicationUser)).Where(it => it.ApplicationUser.Id == id).ToList();
            var output = _mapper.Map<List<VehicleResponse>>(result);
            return output;
        }

        public List<VehicleResponse> GetVehicles()
        {
            var result = _applicationDbContext.Vehicles.Include(nameof(ApplicationUser)).ToList();
            var output = _mapper.Map<List<VehicleResponse>>(result);
            return output;
        }
    }
}
