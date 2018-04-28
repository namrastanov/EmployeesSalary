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
        /// Get the employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EmployeeBaseModel> GetEmployeeAsync(Guid id);

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

        /// <summary>
        /// Get sum of salaries from db and save it to cache
        /// Call only at startup
        /// </summary>
        /// <returns></returns>
        void InitTotalSalary();

        /// <summary>
        /// Delete employee from db by id
        /// </summary>
        /// <returns></returns>
        Task DeleteEmployeeAsync(Guid id);
    }
}
