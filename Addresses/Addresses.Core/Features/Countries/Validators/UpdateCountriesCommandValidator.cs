using Addresses.Core.Features.Countries.Command;
using FluentValidation;


namespace Addresses.Core.Features.Countries.Validators
{
    public class UpdateCountriesCommandValidator : AbstractValidator<UpdateCountriesCommand>
    {
        public UpdateCountriesCommandValidator()
        {
            string patternOnlyLetters = @"[a-zA-ZñÑ\s]$";

            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Name is requited")
                .Matches(patternOnlyLetters).WithMessage("Name format is not valid")
                .MaximumLength(30).WithMessage("Name is not valid");
            RuleFor(d => d.IsActive)
                .Must(x => x == false || x == true);
        }
    }
}
