using EmployeesSalary.Data.Repositories.IRepositories;
using System;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; set; }

        int Commit();

        Task<int> CommitAsync();
    }
}
