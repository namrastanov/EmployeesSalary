using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.Managers.IManagers
{
    public interface IEmployeeImportManager
    {
        /// <summary>
        /// Manager with read excel data operations
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Id of the imported file</returns>
        Task<int> ImportExcelFileAsync(IFormFile file);
    }
}
