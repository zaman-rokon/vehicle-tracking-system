using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using VehicleTrackingSystem.Application.ResponseModels;
using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.Application.Managers
{
    public interface IVehicleManager
    {
        List<VehicleResponse> GetVehicles();
        List<VehicleResponse> GetVehiclesByUserId(string id);
        Task<VehicleLocation> GetVehicleLastLocationAsync(string vehicleId, DateTime locationDate);
        Task<IEnumerable<VehicleLocation>> GetVehicleLocationsAsync(string vehicleId, DateTime startDateTime, DateTime endDateTime);
    }
}
