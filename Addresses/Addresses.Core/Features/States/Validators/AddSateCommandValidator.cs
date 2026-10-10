using Addresses.Core.Features.States.Command;
using FluentValidation;

namespace Addresses.Core.Features.States.Validators
{
    public class AddSateCommandValidator : AbstractValidator<AddStateCommand>
    {

        public AddSateCommandValidator()
        {
            string patternOnlyLetters = @"[a-zA-ZñÑ\s]$";

            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Name is requited")
                .Matches(patternOnlyLetters).WithMessage("Name format is not valid")
                .MaximumLength(30).WithMessage("Name is not valid");
            RuleFor(d => d.CountryId)
                .NotEmpty().WithMessage("CountryId is required")
                .NotNull().WithMessage("CountryId is required,cant be null")
                .GreaterThan(0).WithMessage("CountryId must be great than 0");
            RuleFor(d => d.IsActive)
                .Must(x => x == false || x == true);
        }
    }
}
