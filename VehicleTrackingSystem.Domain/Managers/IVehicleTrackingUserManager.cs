using System.Threading.Tasks;

using VehicleTrackingSystem.Application.ResponseModels;

namespace VehicleTrackingSystem.Application.Managers
{
    public interface IVehicleTrackingUserManager
    {
        Task<TokenResponse> GetUserToken(string email, string password);
        Task<bool> RegisterUser(string email, string password, string firstName, string lastName, string vehicleRegistrationNumber, string vehicleModelName);
        Task<bool> AssignUserRole(string email, string roleName);
        Task<bool> RemoveUserRole(string email, string roleName);
    }
}
