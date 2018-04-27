using EmployeesSalary.Data.Models.BaseModels;
using EmployeesSalary.Data.Validators;
using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeesSalary.Data.Models.Requests
{
    [Validator(typeof(EmployeeValidator))]
    public class UpdateEmployeeRequest : EmployeeBaseModel
    {
        [Required]
        public override Guid Id { get; set; }
    }
}
