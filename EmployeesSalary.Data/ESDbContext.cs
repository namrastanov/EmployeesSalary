using EmployeesSalary.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesSalary.Data
{
    public class ESDbContext: DbContext
    {
        public ESDbContext(DbContextOptions<ESDbContext> options) : base(options)
        {
        }

        DbSet<Employee> Employees { get; set; }
        DbSet<ImportedFile> ImportedFiles { get; set; }
    }
}
