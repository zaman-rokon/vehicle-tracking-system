using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using VehicleTrackingSystem.Application.Managers;
using VehicleTrackingSystem.Application.RequestModels;
using VehicleTrackingSystem.Application.ResponseModels;
using VehicleTrackingSystem.Controllers.ControllerHelpers;
using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.Controllers
{
    [Route("api/administration/")]
    [Authorize(Policy = "AdministrationPolicy")]
    [ApiController]
    public class AdministrationQueryController : AuthorizeControllerBase
    {

        private readonly IVehicleManager _vehicleManager;

        public AdministrationQueryController(IVehicleManager vehicleManager)
        {
            _vehicleManager = vehicleManager;
        }

        /// <summary>
        /// Get all registered vehicles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("vehicles")]
        public IEnumerable<VehicleResponse> GetVehicles()
        {
            var result = _vehicleManager.GetVehicles();
            return result;
        }

        /// <summary>
        /// Get all vehicles by user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}/vehicles")]
        public IEnumerable<VehicleResponse> GetVehiclesById(string userId)
        {
            var result = _vehicleManager.GetVehiclesByUserId(userId);
            return result;
        }

        /// <summary>
        /// Get vehicles locations of specific time duration.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("vehicle/{id}/locations")]
        public async Task<IEnumerable<VehicleLocation>> GetTravelDetail(string id, [FromQuery] LocationDetailRequest request)
        {
            IEnumerable<VehicleLocation> result = new List<VehicleLocation>();
            if (request.StartDateTime.Date.ToUniversalTime().Date == request.EnDateTime.Date.ToUniversalTime().Date)
            {
                if (request.StartDateTime > request.EnDateTime)
                {
                    throw new Exception("Start date can't be grater than end date.");
                }
                result = await _vehicleManager.GetVehicleLocationsAsync(id, request.StartDateTime.ToUniversalTime(), request.EnDateTime.ToUniversalTime());
            }
            else
            {
                throw new Exception("Start date and end date must be same.");
            }

            return result;
        }

        /// <summary>
        /// Get vehicle last location of specific date.
        /// If date is not provided system will take today as last day.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("vehicle/{id}/lastlocation")]
        public async Task<ActionResult<VehicleLocation>> GetLastLocation(string id, [FromQuery] DateTime dateTime)
        {
            if (dateTime == default)
            {
                dateTime = DateTime.Now.ToUniversalTime();
            }
            var result = await _vehicleManager.GetVehicleLastLocationAsync(id, dateTime.ToUniversalTime());

            return Ok(result);
        }

    }
}
