using EmployeesSalary.Data.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.Services.IServices
{
    public interface IImportedFileService: IDisposable
    {
        /// <summary>
        /// Add many employees
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Inserted count</returns>
        Task<int> AddFileAsync(IFormFile file);

        /// <summary>
        /// Check file status in db
        /// </summary>
        /// <param name="fileId">file id</param>
        /// <returns>FileImportStatuses</returns>
        Task<FileImportStatuses> CheckFileStatusAsync(int fileId);

        /// <summary>
        /// Set file import status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task SetFileStatusAsync(int id, FileImportStatuses status);
    }
}
