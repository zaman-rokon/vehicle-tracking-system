using VehicleTrackingSystem.DataAccess.DbContext;
using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.DataAccess.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext applicationDBContext) : base(applicationDBContext)
        {

        }
    }
}
