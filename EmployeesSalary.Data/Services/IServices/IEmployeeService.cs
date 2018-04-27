using EmployeesSalary.Data.Models;
using EmployeesSalary.Data.Models.BaseModels;
using EmployeesSalary.Data.Models.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.Services.IServices
{
    public interface IEmployeeService: IDisposable
    {
        /// <summary>
        /// Get the page of employees
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Paged collection of EmployeeBaseModel</returns>
        Task<IPagedCollection<EmployeeBaseModel>> GetEmployeeListAsync(int page);

        /// <summary>
        /// Add many employees
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Inserted count</returns>
        Task<int> AddEmployeesAsync(IList<AddEmployeeRequest> employeesList);

        /// <summary>
        /// Update the employee info in db
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task UpdateEmployeeInfoAsync(UpdateEmployeeRequest request);

        /// <summary>
        /// Add one employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Guid> AddEmployeeAsync(AddEmployeeRequest request);
    }
}
