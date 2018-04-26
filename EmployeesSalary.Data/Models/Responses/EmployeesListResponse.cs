using EmployeesSalary.Data.Models.BaseModels;
using System.Collections.Generic;

namespace EmployeesSalary.Data.Models.Responses
{
    public class EmployeesListResponse
    {
        public IList<EmployeeBaseModel> EmployeesList { get; set; }
    }
}
