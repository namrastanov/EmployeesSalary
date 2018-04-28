using EmployeesSalary.Data.Models.BaseModels;

namespace EmployeesSalary.Data.Models.Responses
{
    public class EmployeesListResponse
    {
        public IPagedCollection<EmployeeBaseModel> EmployeesList { get; set; }

        public long TotalSalary { get; set; }
    }
}
