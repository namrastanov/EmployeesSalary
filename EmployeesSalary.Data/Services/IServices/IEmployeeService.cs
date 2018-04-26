using EmployeesSalary.Data.Models;
using EmployeesSalary.Data.Models.BaseModels;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.Services.IServices
{
    public interface IEmployeeService
    {
        Task<IPagedCollection<EmployeeBaseModel>> GetEmployees(int page);

        Task<int> UpdateEmployeeInfo(EmployeeBaseModel request);

        Task<EmployeeBaseModel> AddEmployeeAsync(EmployeeBaseModel request);

    }
}
