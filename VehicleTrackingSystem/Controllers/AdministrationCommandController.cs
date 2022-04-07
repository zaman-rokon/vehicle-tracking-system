using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using VehicleTrackingSystem.Application.Commands;
using VehicleTrackingSystem.Controllers.ControllerHelpers;

namespace VehicleTrackingSystem.Controllers
{
    [Route("api/administration/")]
    [Authorize(Policy = "AdministrationPolicy")]
    [ApiController]
    public class AdministrationCommandController : AuthorizeControllerBase
    {

        private readonly IMediator _mediator;

        public AdministrationCommandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Assign user a new role.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserRole")]
        public async Task<IActionResult> AssignUserRole(UserRoleUpdateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Revoke a role from user.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("UserRole")]
        public async Task<IActionResult> RemoveUserRole(UserRoleUpdateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
