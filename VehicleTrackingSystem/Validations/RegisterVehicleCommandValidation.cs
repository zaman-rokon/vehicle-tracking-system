using FluentValidation;

using VehicleTrackingSystem.Application.Commands;

namespace VehicleTrackingSystem.Validations
{
    public class RegisterVehicleCommandValidator : AbstractValidator<RegisterVehicleCommand>
    {
        public RegisterVehicleCommandValidator()
        {
            RuleFor(x => x.RegistrationNumber).NotNull().NotEmpty();
        }
    }
}
