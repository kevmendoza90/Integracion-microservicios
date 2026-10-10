using Business.Core.Features.CutomersTypes.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Core.Features.CutomersTypes.Validators
{
    public class AddCustomersTypeCommandValidator :AbstractValidator<AddCustomersTypesCommand>
    {
        /// <summary>
        /// no debe ser vacio
        /// deben ser solo numeros
        /// no debe ser nulo
        /// </summary>
        public AddCustomersTypeCommandValidator()
        {
            
            string pattern = "[a-zA-ZñÑ\\s]$";
            
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Name is required")
                .Matches(pattern).WithMessage("Name only needs letters , not numbers")
                .MaximumLength(30).WithMessage("Customer Type is no valid.You can only use 30 or less characters");
            RuleFor(d => d.IsActive)
                .Must(x => x == false || x == true);

        }
    }
}
