using System.Net;
using EmployeesSalary.Filters;
using EmployeesSalary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesSalary.Controllers.Api
{
    [ApiModelValidatioin]
    [ApiExceptionHandler]
    public class ApiController : Controller
    {
        /// <summary>
        /// Return HTTP 200
        /// </summary>
        /// <typeparam name="T">Data type in response</typeparam>
        /// <param name="data">Response data</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        protected IActionResult ApiOkResult<T>(T data, string message = "")
        {
            return Ok(new ApiResponse<T> { Data = data, Message = message, Success = true });
        }

        /// <summary>
        /// Return HTTP 201
        /// </summary>
        /// <typeparam name="T">Data type in response</typeparam>
        /// <param name="data">Response data</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        protected IActionResult ApiCreatedResult<T>(T data, string message)
        {
            return GetResult(
                new ApiResponse<T>
                {
                    Success = true,
                    Data = data,
                    Message = message
                },
                StatusCodes.Status201Created);
        }

        /// <summary>
        /// Return HTTP 400
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        protected IActionResult ApiBadRequestResult(string message)
        {
            return BadRequest(new ApiResponse<object> { Message = message, Success = false });
        }

        /// <summary>
        /// Return HTTP 404
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        protected IActionResult ApiNotFoundResult(string message)
        {
            return NotFound(new ApiResponse<object> { Message = message, Success = true });
        }

        private IActionResult GetResult<T>(ApiResponse<T> response, int code)
        {
            return new ObjectResult(response)
            {
                StatusCode = code
            };
        }
    }
}