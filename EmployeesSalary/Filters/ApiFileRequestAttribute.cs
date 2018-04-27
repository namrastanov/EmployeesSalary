using EmployeesSalary.Data.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeesSalary.Filters
{
    public class ApiFileRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {            
            if (context.HttpContext.Request.Form.Files.Count == 0)
            {
                throw new CustomException("There are no files");
            }

            base.OnActionExecuting(context);
        }
    }
}
