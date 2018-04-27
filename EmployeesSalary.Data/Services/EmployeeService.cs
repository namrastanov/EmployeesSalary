using EmployeesSalary.Data.Entities;
using EmployeesSalary.Data.Exceptions;
using EmployeesSalary.Data.Helpers;
using EmployeesSalary.Data.Managers.IManagers;
using EmployeesSalary.Data.Models;
using EmployeesSalary.Data.Models.BaseModels;
using EmployeesSalary.Data.Models.Requests;
using EmployeesSalary.Data.Services.IServices;
using EmployeesSalary.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddEmployeeAsync(AddEmployeeRequest request)
        {
            var draftEmployee = await InsertEmployeeButNotCommitAsync(request);

            await _unitOfWork.CommitAsync();
            CacheHelper.UpdateTotalSalary(draftEmployee.Salary);

            return draftEmployee.Id;
        }

        public async Task<int> AddEmployeesAsync(IList<AddEmployeeRequest> employeesList)
        {
            var totalInsertedCount = 0;
            var insertedForCommit = 0;
            long currentTotalSalary = 0;

            foreach(var employeeModel in employeesList)
            {
                var draftEmployee = await InsertEmployeeButNotCommitAsync(employeeModel);

                currentTotalSalary += draftEmployee.Salary;

                insertedForCommit++;
                totalInsertedCount++;
                if (insertedForCommit >=  100)
                {
                    insertedForCommit = 0;
                    await _unitOfWork.CommitAsync();
                }
            }

            if (insertedForCommit > 0)
            {
                await _unitOfWork.CommitAsync();
            }

            CacheHelper.UpdateTotalSalary(currentTotalSalary);
            return totalInsertedCount;
        }

        public async Task<IPagedCollection<EmployeeBaseModel>> GetEmployeeListAsync(int page)
        {
            var entities = _unitOfWork.EmployeeRepository.GetAll().OrderByDescending(e => e.DateCreated);
            var total = await entities.CountAsync();

            var pageSize = 20;

            var pagedEmployees = entities
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var items = pagedEmployees.Select(e => GetModelFromEntity(e));

            return new PagedCollection<EmployeeBaseModel>(items, total, pageSize);
        }

        public async Task UpdateEmployeeInfoAsync(UpdateEmployeeRequest request)
        {
            var entity = GetEntityFromModel(request);
            entity.Id = request.Id;

            var existEntity = await _unitOfWork.EmployeeRepository.GetByIdAsync(entity.Id);
            if (existEntity == null)
            {
                throw new CustomException("Employee not found");
            } else
            {
                CacheHelper.UpdateTotalSalary(request.Salary - existEntity.Salary);
            }

            _unitOfWork.EmployeeRepository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        private async Task<Employee> InsertEmployeeButNotCommitAsync(EmployeeBaseModel employee)
        {
            var draftEmployee = await _unitOfWork.EmployeeRepository.InsertAsync(GetEntityFromModel(employee));

            return draftEmployee;
        }

        private Employee GetEntityFromModel(EmployeeBaseModel model)
        {
            return new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Salary = model.Salary,
                DateCreated = DateTime.Now,
                ImportedFileId = model.ImportedFileId
            };
        }

        private EmployeeBaseModel GetModelFromEntity(Employee entity)
        {
            return new EmployeeBaseModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                Salary = entity.Salary
            };
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
                _unitOfWork = null;
            }
        }
    }
}
