using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using FluentValidation;

namespace ModelLayer.Validator
{
    public class AddressBookValidator : AbstractValidator<AddressBookDTO>
    {
        public AddressBookValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required."); ;
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid Email format."); ;
            RuleFor(x => x.Phone).Matches(@"^\d{10}$").WithMessage("Invalid Phone number.");
        }
    }
}