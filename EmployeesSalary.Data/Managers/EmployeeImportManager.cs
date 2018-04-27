using EmployeesSalary.Data.Managers.IManagers;
using EmployeesSalary.Data.Models.Requests;
using EmployeesSalary.Data.Services.IServices;
using EmployeesSalary.Data.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeesSalary.Data.Services;
using EmployeesSalary.Data.Entities;
using EmployeesSalary.Data.Repositories;
using System.Collections.Generic;
using EmployeesSalary.Data.Types;
using EmployeesSalary.Data.Extensions;
using Microsoft.Extensions.DependencyInjection;
using EmployeesSalary.Data.UnitOfWork;

namespace EmployeesSalary.Data.Managers
{
    public class EmployeeImportManager : IEmployeeImportManager
    {
        private readonly IEmployeeService _employeeService;
        private readonly IImportedFileService _importedFileService;
        private readonly ILogger _logger;
        private readonly IOptions<DbOption> _dbOption;

        public EmployeeImportManager(
            IEmployeeService employeeService,
            IImportedFileService importedFileService,
            ILoggerFactory loggerFactory,
            IOptions<DbOption> dbOption
            )
        {
            _employeeService = employeeService;
            _importedFileService = importedFileService;
            _logger = loggerFactory.CreateLogger("Default");
            _dbOption = dbOption;
        }

        public async Task<int> ImportExcelFileAsync(IFormFile file)
        {
            var fileId = await _importedFileService.AddFileAsync(file);
            _logger.LogInformation($"{file.FileName} importing started");

            using (var stream = file.OpenReadStream())
            {
                var excelPackage = new ExcelPackage(stream);

                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[1];
                var rowCount = workSheet.Dimension.End.Row;
                var columnCount = workSheet.Dimension.End.Column;

                var thread = new Thread(async () => await ProcessFileAsync(fileId, workSheet, 2, rowCount))
                {
                    Name = "FileProcessThread"
                };
                thread.Start();
            }
            return fileId;
        }

        private async Task ProcessFileAsync(int fileId, ExcelWorksheet workSheet, int startRow, int endRow)
        {
            // TODO Use Autofac instead .NET Core built-in container
            var serviceCollection = new ServiceCollection();
            serviceCollection.ApplyEmployeesSalaryBindings(_dbOption.Value.ESDbConnection);

            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            using (var dbContext = (DbContext)serviceProvider.GetService(typeof(DbContext)))
            using (var unitOfWork = (IUnitOfWork)serviceProvider.GetService(typeof(IUnitOfWork)))
            using (var @employeeService = (IEmployeeService)serviceProvider.GetService(typeof(IEmployeeService)))
            using (var @importedFileService = (IImportedFileService)serviceProvider.GetService(typeof(IImportedFileService)))
            {
                var collectionForInsert = new List<AddEmployeeRequest>();
                for (int i = startRow; i <= endRow; i++)
                {
                    var newEmployee = new AddEmployeeRequest
                    {
                        ImportedFileId = fileId
                    };

                    for (int j = 1; j <= 4; j++)
                    {
                        try
                        {
                            switch (j)
                            {
                                case 1:
                                    {
                                        newEmployee.FirstName = workSheet.Cells[i, j].Value?.ToString();
                                        break;
                                    }
                                case 2:
                                    {
                                        newEmployee.LastName = workSheet.Cells[i, j].Value?.ToString();
                                        break;
                                    }
                                case 3:
                                    {
                                        newEmployee.PhoneNumber = workSheet.Cells[i, j].Value?.ToString();
                                        break;
                                    }
                                case 4:
                                    {
                                        newEmployee.Salary = workSheet.Cells[i, j].GetValue<int>();
                                        break;
                                    }
                                default:
                                    {
                                        continue;
                                    }
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }

                    var validator = new EmployeeValidator();
                    ValidationResult results = validator.Validate(newEmployee);
                    if (!results.IsValid)
                    {
                        _logger.LogError($"{newEmployee.GetLogStr()} - is not valid");
                    }
                    else
                    {
                        _logger.LogInformation($"Success read employee: {newEmployee.GetLogStr()}");
                        collectionForInsert.Add(newEmployee);

                        if (collectionForInsert.Count == 100)
                        {
                            await @employeeService.AddEmployeesAsync(collectionForInsert);
                            collectionForInsert.Clear();
                        }
                    }
                }

                if (collectionForInsert.Count > 0)
                {
                    await @employeeService.AddEmployeesAsync(collectionForInsert);
                    collectionForInsert.Clear();
                }
                await @importedFileService.SetFileStatusAsync(fileId, FileImportStatuses.Finished);
            }
        }
    }
}
