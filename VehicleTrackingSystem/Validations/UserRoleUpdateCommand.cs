using FluentValidation;

using VehicleTrackingSystem.Application;
using VehicleTrackingSystem.Application.Commands;

namespace VehicleTrackingSystem.Validations
{
    public class UserRoleUpdateCommandValidation : AbstractValidator<UserRoleUpdateCommand>
    {
        public UserRoleUpdateCommandValidation()
        {
            RuleFor(x => x.RoleName).NotEmpty().IsEnumName(typeof(ApplicationRole));
        }
    }
}
