using EmployeesSalary.Data.Models.BaseModels;
using EmployeesSalary.Data.Validators;
using FluentValidation.Attributes;

namespace EmployeesSalary.Data.Models.Requests
{
    [Validator(typeof(EmployeeValidator))]
    public class AddEmployeeRequest: EmployeeBaseModel
    {
        public string GetLogStr()
        {
            return $"{FirstName};{LastName};{PhoneNumber};{Salary}";
        }
    }
}
