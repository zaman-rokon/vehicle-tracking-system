using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using VehicleTrackingSystem.Application;

namespace VehicleTrackingSystem.Controllers.ControllerHelpers
{
    public class AuthorizeControllerBase : ControllerBase
    {
        public string UserId
        {
            get
            {
                var data = User.Claims.First(claim => claim.Type == VehicleTrackingSystemConstant.AUTHORIZATION_CLAIM_USERID).Value;
                if (data == null)
                {
                    throw new Exception("Unauthorized Access.");
                }

                return data;
            }
        }

        public string VehicleId
        {
            get
            {
                var data = User.Claims.First(claim => claim.Type == VehicleTrackingSystemConstant.AUTHORIZATION_CLAIM_VEHICLE).Value;
                if (data == null)
                {
                    throw new Exception("Unauthorized Vehicle.");
                }

                return data;
            }
        }

        public List<string> Roles
        {
            get; set;
        }

    }
}
