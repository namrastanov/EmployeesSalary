using EmployeesSalary.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesSalary.Data
{
    public class ESDbContext: DbContext
    {
        public ESDbContext()
        {

        }

        DbSet<Employee> Employees { get; set; }
    }
}
