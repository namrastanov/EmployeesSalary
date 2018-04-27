using EmployeesSalary.Data.Models.BaseModels;
using FluentValidation;

namespace EmployeesSalary.Data.Validators
{
    public class EmployeeValidator: AbstractValidator<EmployeeBaseModel>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                . NotEmpty().WithMessage("The First Name cannot be blank.")
                .Length(0, 100).WithMessage("The First Name cannot be more than 100 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("The Last Name cannot be blank.")
                .Length(0, 100).WithMessage("The Last Name cannot be more than 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .Length(7, 20).WithMessage("The Phone number must be at least 7 characters long.")
                .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").WithMessage("Invalid Phone number format");

            RuleFor(x => x.Salary)
                .NotEmpty().WithMessage("The First Name cannot be blank.")
                .GreaterThanOrEqualTo(0).WithMessage("The Salary can not be a negative number.");
        }
    }
}
