using Addresses.Core.Features.Positions.Command;
using FluentValidation;


namespace Addresses.Core.Features.Positions.Validators
{
    public class AddPositionCommandValidator : AbstractValidator<AddPositionCommand>
    {
        public AddPositionCommandValidator()
        {
            RuleFor(d => d.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required")
                .NotNull().WithMessage("ZipCode is requited")
                .MaximumLength(10).WithMessage("ZipCode is not valid");
            RuleFor(d => d.CityId)
                .NotEmpty().WithMessage("CityId is required")
                .NotNull().WithMessage("CityId is requited")
                .GreaterThan(0).WithMessage("CityId is invalid");
            RuleFor(d => d.Address)
                .MaximumLength(300).WithMessage("Address is not valid")
                .NotEmpty().WithMessage("Address is required")
                .NotNull().WithMessage("Address is requited");
                
            RuleFor(d => d.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required")
                .NotNull().WithMessage("CustomerId is requited")
                .GreaterThan(0).WithMessage("CustomerId is invalid");
        }
    }
}
