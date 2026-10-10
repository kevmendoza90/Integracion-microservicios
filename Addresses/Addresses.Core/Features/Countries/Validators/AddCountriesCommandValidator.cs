using Addresses.Core.Features.Countries.Command;
using FluentValidation;


namespace Addresses.Core.Features.Countries.Validators
{
    public class AddCountriesCommandValidator : AbstractValidator<AddCountriesCommand>
    {
        public AddCountriesCommandValidator()
        {
            string patternOnlyLetters = @"[a-zA-ZñÑ\s]$";

            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Name is requited")
                .Matches(patternOnlyLetters).WithMessage("Name format is not valid")
                .MaximumLength(30).WithMessage("Name is not valid");
        }
    }
}
