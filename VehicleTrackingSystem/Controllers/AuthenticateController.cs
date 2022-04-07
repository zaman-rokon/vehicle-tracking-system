using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VehicleTrackingSystem.Application.Commands;
using VehicleTrackingSystem.Application.ResponseModels;
using MediatR;

namespace VehicleTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Provide Email and Password to get JWT token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LogInCommand model)
        {
            var response = await _mediator.Send(model);
            if (response != null)
            {
                return Ok(response);
            }
            return Unauthorized();
        }


        /// <summary>
        /// Register a user with vehicle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationCommand model)
        {
            var response = await _mediator.Send(model);
            if (response)
            {
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
