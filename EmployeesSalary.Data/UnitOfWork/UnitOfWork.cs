using EmployeesSalary.Data.Entities;
using EmployeesSalary.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IGenericRepository<ImportedFile, int> ImportedFileRepository { get; set; }

        private DbContext _context;

        public UnitOfWork(
            DbContext context,
            IEmployeeRepository employeeRepository,
            IGenericRepository<ImportedFile, int> importedFileRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            EmployeeRepository = employeeRepository;
            ImportedFileRepository = importedFileRepository;
        }

        public int Commit()
        {
            if (_context == null)
            {
                throw new ObjectDisposedException("context");
            }

            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            if (_context == null)
            {
                throw new ObjectDisposedException("context");
            }

            return await _context.SaveChangesAsync();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
