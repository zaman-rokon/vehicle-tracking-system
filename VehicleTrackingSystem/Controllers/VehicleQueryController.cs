using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VehicleTrackingSystem.Application.Managers;
using VehicleTrackingSystem.Application.RequestModels;
using VehicleTrackingSystem.Controllers.ControllerHelpers;
using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.Controllers
{
    [Route("api/")]
    [ApiController]
    public class VehicleQueryController : AuthorizeControllerBase
    {
        private readonly IVehicleManager _vehicleManager;

        public VehicleQueryController(IVehicleManager vehicleManager)
        {
            _vehicleManager = vehicleManager;
        }

        /// <summary>
        /// Get all vehicles by user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("vehicle")]
        public IActionResult GetRegisteredVehicle()
        {
            var result = _vehicleManager.GetVehiclesByUserId(UserId);
            return Ok(result);
        }


        /// <summary>
        /// Get vehicle last location of specific date.
        /// If date is not provided system will take today as last day.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("vehicle/lastlocation")]
        public async Task<VehicleLocation> GetLastLocation([FromQuery] DateTime dateTime)
        {
            if (dateTime == default)
            {
                dateTime = DateTime.Now.ToUniversalTime();
            }
            var result = await _vehicleManager.GetVehicleLastLocationAsync(VehicleId, dateTime.ToUniversalTime());

            return result;
        }

        /// <summary>
        /// Get vehicles locations of specific time duration.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("vehicle/locations")]
        public async Task<ActionResult<IEnumerable<VehicleLocation>>> GetTravelDetail([FromQuery] LocationDetailRequest request)
        {

            IEnumerable<VehicleLocation> result;
            if (request.StartDateTime.ToUniversalTime().Date == request.EnDateTime.ToUniversalTime().Date)
            {
                if (request.StartDateTime > request.EnDateTime)
                {
                    throw new Exception("Start date can't be grater than end date.");
                }
                result = await _vehicleManager.GetVehicleLocationsAsync(VehicleId, request.StartDateTime.ToUniversalTime(), request.EnDateTime.ToUniversalTime());
            }
            else
            {
                throw new Exception("Start date and end date must be same.");
            }

            return Ok(result);
        }

    }
}
