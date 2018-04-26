using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesSalary.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        [Route("/list")]
        public async Task<IActionResult> GetEmployeeListAsync()
        {
            return Ok();
        }
    }
}