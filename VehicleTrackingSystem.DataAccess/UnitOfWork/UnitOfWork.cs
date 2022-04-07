using VehicleTrackingSystem.DataAccess.DbContext;
using VehicleTrackingSystem.DataAccess.Repositories;

namespace VehicleTrackingSystem.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public IVehicleRepository Vehicles { get; private set; }
        public IApplicationUserRepository ApplicationUsers { get; private set; }

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            Vehicles = new VehicleRepository(_applicationDbContext);
            ApplicationUsers = new ApplicationUserRepository(_applicationDbContext);
        }

        public int Complete()
        {
            return _applicationDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}
