using Business.Core.Features.CustomersContacts.Command;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Business.Core.Features.CustomersContacts.Validators
{
    class UpdateCustomersContactsCommandValidator : AbstractValidator<UpdateCustomerContactCommand>
    {
        public UpdateCustomersContactsCommandValidator()
        {
            string patternOnlyLetters = "[a-zA-ZñÑ\\s]$";
            string emailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                         + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                         + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            
            string patternPhoneNumber = @"^(\(\+?\d{2,3}\)[\*|\s|\-|\.]?(([\d][\*|\s|\-|\.]?){6})(([\d][\s|\-|\.]?){2})?|(\+?[\d][\s|\-|\.]?){8}(([\d][\s|\-|\.]?){2}(([\d][\s|\-|\.]?){2})?)?)$";
            Regex rgxPatternPhoneNumber = new(patternPhoneNumber);


            RuleFor(d => d.FirstName)
                .NotNull().WithMessage("FirstName is required")
                .NotEmpty().WithMessage("FirstName ir required, cant be null")
                .Matches(patternOnlyLetters).WithMessage("FirstName only needs letters , not numbers")
                .MaximumLength(30).WithMessage("You can only use 30 or less characters");

            RuleFor(d => d.LastName)
                .NotEmpty().WithMessage("LastName is required")
                .NotNull().WithMessage("LastName is required,cant be null")
                .Matches(patternOnlyLetters).WithMessage("LastName only need letters,not numbers")
                .MaximumLength(30).WithMessage("You can only use 30 or less characters");

            RuleFor(d => d.Email)
                .NotEmpty().WithMessage("Email is required")
                .NotNull().WithMessage("Email is required, cant be null")
                .EmailAddress().WithMessage("Email is not valid email address")
                .Matches(emailPattern).WithMessage("Email is not valid email address");
            RuleFor(d => d.Phone1)
                .NotEmpty().WithMessage("Phone1 is required")
                .NotNull().WithMessage("Phone1 is required,cant be null")
                .Matches(rgxPatternPhoneNumber).WithMessage("Phone1 format is invalid");
            RuleFor(d => d.Phone2)
                .NotEmpty().WithMessage("Phone2 is required")
                .NotNull().WithMessage("Phone2 is required,cant be null")
                .Matches(rgxPatternPhoneNumber).WithMessage("Phone2 format is invalid");
            RuleFor(d => d.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required")
                .NotNull().WithMessage("CustomerId is required,cant be null")
                .GreaterThan(0).WithMessage("CustomerId must be great than 0");

            RuleFor(d => d.IsActive)
                .Must(x => x == false || x == true);
        }
    }
}
