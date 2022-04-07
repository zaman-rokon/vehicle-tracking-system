using VehicleTrackingSystem.DataAccess.DbContext;
using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.DataAccess.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext applicationDBContext) : base(applicationDBContext)
        {
        }

    }
}
