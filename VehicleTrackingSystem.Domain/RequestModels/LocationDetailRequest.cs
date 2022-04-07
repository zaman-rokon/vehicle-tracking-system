using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.Cosmos;

namespace VehicleTrackingSystem.Application.RequestModels
{
    public class LocationDetailRequest
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EnDateTime { get; set; }
    }
}
