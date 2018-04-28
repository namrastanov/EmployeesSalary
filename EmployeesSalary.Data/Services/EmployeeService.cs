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
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork _unitOfWork;
        private IMemoryCache _cache;

        public EmployeeService(
            IUnitOfWork unitOfWork,
            IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Guid> AddEmployeeAsync(AddEmployeeRequest request)
        {
            var draftEmployee = await InsertEmployeeButNotCommitAsync(request);

            await _unitOfWork.CommitAsync();
            CacheHelper.AddToTotalSalary(draftEmployee.Salary);

            return draftEmployee.Id;
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var salary = (await _unitOfWork.EmployeeRepository.GetByIdAsync(id))?.Salary;

            if (salary.HasValue)
            {
                CacheHelper.AddToTotalSalary(-1 * salary.Value);
            }

            await _unitOfWork.EmployeeRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
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

            CacheHelper.AddToTotalSalary(currentTotalSalary);
            return totalInsertedCount;
        }

        public async Task<EmployeeBaseModel> GetEmployeeAsync(Guid id)
        {
            var entity = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            return GetModelFromEntity(entity);
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
            var existSalary = _unitOfWork.EmployeeRepository.Query(e => e.Id == request.Id).FirstOrDefault()?.Salary;
            CacheHelper.AddToTotalSalary(request.Salary - existSalary.Value);

            var entity = GetEntityFromModel(request);
            entity.Id = request.Id;
            _unitOfWork.EmployeeRepository.ApplyCurrentValues(
                entity, 
                e => e.FirstName,
                e => e.LastName,
                e => e.PhoneNumber,
                e => e.Salary);

            await _unitOfWork.CommitAsync();
        }

        public void InitTotalSalary()
        {
            var totalSalary = CacheHelper.GetTotalSalary();

            if (totalSalary == 0)
            {
                totalSalary = _unitOfWork.EmployeeRepository.GetAll().Sum(e => long.Parse(e.Salary.ToString()));
                CacheHelper.AddToTotalSalary(totalSalary);
            }
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
