using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesSalary.Controllers.Mvc
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}