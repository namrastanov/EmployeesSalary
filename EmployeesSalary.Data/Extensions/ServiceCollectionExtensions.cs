using EmployeesSalary.Data.Entities;
using EmployeesSalary.Data.Managers;
using EmployeesSalary.Data.Managers.IManagers;
using EmployeesSalary.Data.Models.BaseModels;
using EmployeesSalary.Data.Repositories;
using EmployeesSalary.Data.Repositories.IRepositories;
using EmployeesSalary.Data.Services;
using EmployeesSalary.Data.Services.IServices;
using EmployeesSalary.Data.UnitOfWork;
using EmployeesSalary.Data.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeesSalary.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ApplyEmployeesSalaryBindings(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ESDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<DbContext, ESDbContext>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IGenericRepository<ImportedFile, int>, GenericRepository<ImportedFile, int>>();
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IImportedFileService, ImportedFileService>();

            services.AddTransient<IValidator<EmployeeBaseModel>, EmployeeValidator>();
            services.AddTransient<IEmployeeImportManager, EmployeeImportManager>();
        }
    }
}
