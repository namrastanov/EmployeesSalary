using EmployeesSalary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeesSalary.Filters
{
    public class ApiModelValidatioinAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var response = new ApiResponse<object>
                {
                    Success = false,
                    Message = string.Join(',', context.ModelState?.Values)
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
