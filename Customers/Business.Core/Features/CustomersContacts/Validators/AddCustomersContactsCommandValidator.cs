using Business.Core.Features.CustomersContacts.Command;
using FluentValidation;
using System.Text.RegularExpressions;


namespace Business.Core.Features.CustomersContacts.Validators
{
    public class AddCustomersContactsCommandValidator : AbstractValidator<AddCustomerContactCommand>
    {

        /// <summary>
        /// pattern es un regex para solo letras y espacios en blanco
        /// ^[a-zA-Z0-9À-ÿ\u00f1\u00d1]+(\s[a-zA-Z0-9À-ÿ\u00f1\u00d1]+)$ es para dejar solo un espacio entre dos nombres,
        /// no despues del segundo ni antes del primero  y solo admite uno o dos nombres con un espacio de por medio.
        /// Los formatos del numero de telefono son: (503)77887700 , (+503) 7790-5050, (+503) 77-90-50-50,(503) 77-90-50-50,(503) 7790-5050 7777-7777
        /// string patternMiddleSpace = "^[a-zA-Z0-9À-ÿ\u00f1\u00d1]+(\\s[a-zA-Z0-9À-ÿ\u00f1\u00d1]+)$";
        /// string patternOnlyNumbers = "[0 - 9]{ 1,9} (\\.[0 - 9]{ 0,2})?$";
        /// </summary>
        public AddCustomersContactsCommandValidator()
        {
            string patternOnlyLetters = "[a-zA-ZñÑ\\s]$";
            
            string emailPattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            Regex rgxEmailpattern = new(emailPattern);
            string patternPhoneNumber = @"^(\(\+?\d{2,3}\)[\*|\s|\-|\.]?(([\d][\*|\s|\-|\.]?){6})(([\d][\s|\-|\.]?){2})?|(\+?[\d][\s|\-|\.]?){8}(([\d][\s|\-|\.]?){2}(([\d][\s|\-|\.]?){2})?)?)$";
            Regex rgxPatternPhoneNumber = new (patternPhoneNumber);


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
                .Matches(rgxEmailpattern).WithMessage("Email is not valid email address");
            RuleFor(d => d.Phone1)
                .NotEmpty().WithMessage("Phone1 is required")
                .NotNull().WithMessage("Phone1 is required,cant be null")
                .Matches(rgxPatternPhoneNumber).WithMessage("Phone1 format is invalid")
                .MaximumLength(25).WithMessage("Phone1 is not valid");
            RuleFor(d => d.Phone2)
                .NotEmpty().WithMessage("Phone2 is required")
                .NotNull().WithMessage("Phone2 is required,cant be null")
                .Matches(rgxPatternPhoneNumber).WithMessage("Phone2 format is invalid")
                .MaximumLength(25).WithMessage("Phone2 is not valid");
            RuleFor(d => d.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required")
                .NotNull().WithMessage("CustomerId is required,cant be null")
                .GreaterThan(0).WithMessage("CustomerId must be great than 0");
            RuleFor(d => d.IsActive)
                .Must(x => x == false || x == true).WithMessage("Is Active must be false or true");

        }
    }
}
