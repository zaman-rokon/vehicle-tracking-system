using System;

using VehicleTrackingSystem.DataAccess.Repositories;

namespace VehicleTrackingSystem.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleRepository Vehicles { get; }
        IApplicationUserRepository ApplicationUsers { get; }

        int Complete();
    }
}
