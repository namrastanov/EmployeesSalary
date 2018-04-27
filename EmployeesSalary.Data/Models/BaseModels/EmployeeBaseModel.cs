using System;

namespace EmployeesSalary.Data.Models.BaseModels
{
    public class EmployeeBaseModel
    {
        public virtual Guid Id { get; set; }

        public int Salary { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public int ImportedFileId { get; set; }
    }
}
