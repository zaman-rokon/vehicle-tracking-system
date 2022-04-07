using System.Security.Cryptography.X509Certificates;

using AutoMapper;

using VehicleTrackingSystem.Application.Commands;
using VehicleTrackingSystem.Application.ResponseModels;
using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVehicleCommand, Vehicle>();
            CreateMap<Vehicle, VehicleResponse>()
                .ForMember(dest => dest.OwnerName,
                    act => act.MapFrom(src => src.ApplicationUser));
        }
    }
}
