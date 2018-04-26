using EmployeesSalary.Data.Entities;
using System;

namespace EmployeesSalary.Data.Repositories.IRepositories
{
    public interface IEmployeeRepository: IGenericRepository<Employee, Guid>
    {
    }
}
