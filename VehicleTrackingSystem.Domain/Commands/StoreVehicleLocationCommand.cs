using MediatR;

using System;
using Newtonsoft.Json.Linq;

namespace VehicleTrackingSystem.Application.Commands
{
    public class StoreVehicleLocationCommand : IRequest
    {
        public string UserId { get; set; }
        public string VehicleId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime TrackDateTime { get; set; }
        public string CustomData { get; set; }
    }
}
