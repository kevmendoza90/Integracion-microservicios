using Business.Core.Features.Companies.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Core.Features.Companies.Validators
{
    public class AddCompaniesCommandValidator : AbstractValidator<AddCompaniesCommand>
    {


        /// <summary>
        /// Se va a usar el regex /^[^\s@]+@[^\s@]+\.[^\s@]+$/ para validacion de correos electronicos mas comunes, en caso de necesitar una que capture aun mas se
        /// puede usar : /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/ 
        /// recomendada en especificacion de html5 y que admite todos los casos del estandar RFC 5322
        /// </summary>
        public AddCompaniesCommandValidator()
        {
            string emailPattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            Regex rgxEmailpattern = new(emailPattern);

            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Name cannot be null")
                .MaximumLength(30).WithMessage("The length for this field is 30 characters");
            RuleFor(d => d.Sigla)
                .NotNull().WithMessage("Sigla is required")
                .NotEmpty().WithMessage("Sigla cannot be null")
                .MaximumLength(10).WithMessage("The length for this field is 10 characters");
            RuleFor(d => d.MainEmail)
                .NotEmpty().WithMessage("Email is required")
                .NotNull().WithMessage("Email cannot be null")
                .EmailAddress().WithMessage("Email is not valid email address")
                .Matches(rgxEmailpattern).WithMessage("Email format is not valid");
            RuleFor(d => d.IsActive)
                .Must(x => x == false || x == true);

        }
    }
}
