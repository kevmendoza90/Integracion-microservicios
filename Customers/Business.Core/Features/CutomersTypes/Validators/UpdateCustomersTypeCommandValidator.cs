using Business.Core.Features.CutomersTypes.Command;
using FluentValidation;

namespace Business.Core.Features.CutomersTypes.Validators
{
    class UpdateCustomersTypeCommandValidator : AbstractValidator<UpdateCustomersTypesCommand>
    {
        public UpdateCustomersTypeCommandValidator()
        {
            string patternOnlyLettersAndFourWords = @"^[a - zA - Z] +\s[a - zA - Z] +\s[a - zA - Z] +\s[a - zA - Z] +$ ";

            RuleFor(d => d.Name)
                    .NotEmpty().WithMessage("Name is required")
                    .NotNull().WithMessage("Name is required")
                    .Matches(patternOnlyLettersAndFourWords).WithMessage("Name only needs letters , not numbers")
                    .MaximumLength(30).WithMessage("Customer Type is no valid.You can only use 30 or less characters");
            RuleFor(d => d.IsActive)
                .Must(x => x == false || x == true);
        }
        
        
    }
}
