using EmployeesSalary.Data.Exceptions;
using EmployeesSalary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Text;

namespace EmployeesSalary.Filters
{
    public class ApiExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

#if DEBUG
            var errors = new StringBuilder();
            var exceptioin = context.Exception;
            while (exceptioin != null)
            {
                errors.AppendLine(exceptioin.Message);
                exceptioin = exceptioin.InnerException;
                if (exceptioin != null)
                {
                    errors.AppendLine(exceptioin.Message);
                } 
            }

            var response = IsCustomException(context.Exception)
                                       ? new ApiResponse<object> { Message = context.Exception.Message, Data = context.Exception.Data }
                                       : new ApiResponse<object> { Message = errors.ToString() };

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new JsonResult(response);
#else
            var response = IsCustomException(context.Exception)
                                       ? new ApiResponse<object> { Message = context.Exception.Message, Data = context.Exception.Data }
                                       : new ApiResponse<object> { Message = "Unknown error" };

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new JsonResult(response);
#endif

            base.OnException(context);
        }

        private bool IsCustomException(Exception exception)
        {
            return exception is CustomException;
        }

    }
}
