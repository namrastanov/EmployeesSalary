using System;
using System.Threading.Tasks;
using EmployeesSalary.Data.Helpers;
using EmployeesSalary.Data.Managers.IManagers;
using EmployeesSalary.Data.Models.Requests;
using EmployeesSalary.Data.Models.Responses;
using EmployeesSalary.Data.Services.IServices;
using EmployeesSalary.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesSalary.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IImportedFileService _importedFileService;
        private readonly IEmployeeImportManager _employeeImportManager;

        public EmployeeController(
            IEmployeeService employeeService,
            IImportedFileService importedFileService,
            IEmployeeImportManager employeeImportManager
            )
        {
            _employeeService = employeeService;
            _importedFileService = importedFileService;
            _employeeImportManager = employeeImportManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid id)
        {
            var employee = await _employeeService.GetEmployeeAsync(id);

            return ApiOkResult(employee);
        }

        [HttpGet("list/{page}")]
        public async Task<IActionResult> GetEmployeeListAsync(int page)
        {
            var employeesList = await _employeeService.GetEmployeeListAsync(page);
            var response = new EmployeesListResponse
            {
                EmployeesList = employeesList,
                TotalSalary = CacheHelper.GetTotalSalary()
            };

            return ApiOkResult(response);
        }

        [HttpGet("totalSalary")]
        public IActionResult GetTotalSalary()
        {
            var totalSalary = CacheHelper.GetTotalSalary();
            return ApiOkResult(totalSalary);
        }

        [HttpGet("fileStatus/{fileId}")]
        public async Task<IActionResult> GetFileStatusAsync(int fileId)
        {
            var status = await _importedFileService.CheckFileStatusAsync(fileId);
            return ApiOkResult(status);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync(AddEmployeeRequest request)
        {
            var id = await _employeeService.AddEmployeeAsync(request);
            return ApiOkResult(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return ApiOkResult(id);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            await _employeeService.UpdateEmployeeInfoAsync(request);
            return ApiOkResult(true);
        }

        [HttpPost("upload")]
        [ApiFileRequest]
        public async Task<IActionResult> UploadAsync()
        {
            var fileId = await _employeeImportManager.ImportExcelFileAsync(HttpContext.Request.Form.Files[0]);
            return ApiOkResult(fileId);
        }
    }
}