using System;

namespace EmployeesSalary.Data.Models.BaseModels
{
    public class EmployeeBaseModel
    {
        public Guid Id { get; set; }

        public int Salary { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
