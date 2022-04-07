using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.DataAccess.CosmosRepositories
{
    public interface IVehicleLocationRepository
    {
        Task<IEnumerable<VehicleLocation>> GetAllAsync(string vehicleId, DateTime startDate, DateTime endDate);
        Task<VehicleLocation> GetLastLocationAsync(string vehicleId, DateTime locationDate);
    }
}
