using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeDocumentation.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeDocumentation.Data;
using EmployeeDocumentation.Models.TeamViewModels;

namespace EmployeeDocumentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly TeamContext _context;

        public HomeController(TeamContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> About()
        {
            IQueryable<EmployeeGroup> data =
                from employee in _context.Employees
                group employee by employee.SupervisorID into supervisorGroup
                select new EmployeeGroup()
                {
                    SupervisorID = supervisorGroup.Key,
                    EmployeeCount = supervisorGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
