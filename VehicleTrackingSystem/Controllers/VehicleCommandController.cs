using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VehicleTrackingSystem.Application.Commands;
using VehicleTrackingSystem.Controllers.ControllerHelpers;

namespace VehicleTrackingSystem.Controllers
{
    /// <summary>
    /// Vehicle command controller for User.
    /// </summary>
    [Route("api/")]
    [ApiController]
    public class VehicleCommandController : AuthorizeControllerBase
    {
        private readonly IMediator _mediator;
        public VehicleCommandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Register a vehicle.
        /// RegistrationNumber must be unique through out our system.
        /// </summary>
        /// <param name="updateVehicleCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("vehicle/register")]
        public async Task<IActionResult> RegisterVehicle(RegisterVehicleCommand updateVehicleCommand)
        {
            updateVehicleCommand.UserId = UserId;
            var result = await _mediator.Send(updateVehicleCommand);
            return Ok(result);
        }

        /// <summary>
        /// Save current location latitude and longitude.
        /// Content will store extra data need to store in our system.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("vehicle/location/{latitude}/{longitude}")]
        public async Task<IActionResult> SaveLocation(decimal latitude, decimal longitude, [FromBody] object content)
        {
            StoreVehicleLocationCommand command = new StoreVehicleLocationCommand
            {
                Latitude = latitude,
                Longitude = longitude,
                UserId = UserId,
                VehicleId = VehicleId,
                CustomData = JObject.Parse(content.ToString()).ToString(),
                TrackDateTime = System.DateTime.UtcNow
            };
            await _mediator.Send(command);
            return Ok();
        }


    }
}
