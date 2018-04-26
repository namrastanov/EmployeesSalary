using EmployeesSalary.Data.Entities;
using EmployeesSalary.Data.Repositories.IRepositories;
using System;

namespace EmployeesSalary.Data.Repositories
{
    public class EmployeeRepository: GenericRepository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(ESDbContext dbContext): base(dbContext)
        {

        }
    }
}
