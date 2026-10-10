using Business.Core.Features.Companies.Command;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Business.Core.Features.Companies.Validators
{
    public class UpdateCompaniesCommandValidator : AbstractValidator<UpdateCompaniesCommand>
    {
        public UpdateCompaniesCommandValidator()

        {
            string emailPattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            Regex rgxEmailpattern = new(emailPattern);

            RuleFor(d => d.Id)
                .NotEmpty().WithMessage("Id is required")
                .NotNull().WithMessage("Id is requited");
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Name is requited")
                .MaximumLength(30).WithMessage("Name is not valid");
            RuleFor(d => d.Sigla)
                .NotNull().WithMessage("Sigla is required")
                .NotEmpty().WithMessage("Sigla is required")
                .MaximumLength(10).WithMessage("Sigla is not valid"); 
            RuleFor(d => d.MainEmail)
                .NotEmpty().WithMessage("Email is required")
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .Matches(rgxEmailpattern).WithMessage("Email is not valid")
                .MaximumLength(76).WithMessage(d=> $"The length of ‘Email’ must be 76 characters or fewer. You entered 76 or more characters");
            RuleFor(d => d.IsActive)
                .Must(x => x == false || x == true);
        }
    }
}
