using EmployeesSalary.Data.Entities;
using EmployeesSalary.Data.Models;
using EmployeesSalary.Data.Models.BaseModels;
using EmployeesSalary.Data.Services.IServices;
using EmployeesSalary.Data.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public Task<EmployeeBaseModel> AddEmployeeAsync(EmployeeBaseModel request)
        {
            var employee = _unitOfWork.EmployeeRepository.InsertAsync(new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Salary = request.Salary
            });

            GetEmployeeBaseModel(employee, )
        }

        public Task<IPagedCollection<EmployeeBaseModel>> GetEmployees(int page)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateEmployeeInfo(EmployeeBaseModel request)
        {
            throw new NotImplementedException();
        }
    }
}
