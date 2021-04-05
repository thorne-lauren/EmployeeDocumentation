using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeDocumentation.Data;
using EmployeeDocumentation.Models;

namespace EmployeeDocumentation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly TeamContext _context;

        public EmployeesController(TeamContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string sortOrder)
        {
            var employees = _context.Employees
                .Include(s => s.Supervisor)
                .AsNoTracking();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["LastNameSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder != "lastname" ? "lastname" : "lastname_desc";
            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder != "firstname" ? "firstname" : "firstname_desc";
            ViewData["SupervisorSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder != "supervisor" ? "supervisor" : "supervisor_desc";

            switch (sortOrder)
            {
                case "lastname":
                    employees = employees.OrderBy(s => s.LastName);
                    break;
                case "lastname_desc":
                    employees = employees.OrderByDescending(s => s.LastName);
                    break;
                case "firstname":
                    employees = employees.OrderBy(s => s.FirstName);
                    break;
                case "firstname_desc":
                    employees = employees.OrderByDescending(s => s.FirstName);
                    break;
                case "supervisor":
                    employees = employees.OrderBy(s => s.Supervisor);
                    break;
                case "supervisor_desc":
                    employees = employees.OrderByDescending(s => s.Supervisor);
                    break;
                default:
                    employees = employees.OrderBy(s => s.LastName);
                    break;
            }

            return View(await employees.AsNoTracking().ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(s => s.Supervisor)
                .Include(d => d.Documentations)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EmployeeID == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            PopulateSupervisorsDropDownList();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,LastName,FirstName,HireDate,Extension,SupervisorID")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            PopulateSupervisorsDropDownList(employee.SupervisorID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }
            PopulateSupervisorsDropDownList(employee.SupervisorID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeToUpdate = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeID == id);

            if (await TryUpdateModelAsync<Employee>(employeeToUpdate,
                "",
                e => e.EmployeeID, e => e.LastName, e => e.FirstName, e => e.HireDate, e => e.Extension, e => e.SupervisorID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateSupervisorsDropDownList(employeeToUpdate.SupervisorID);
            return View(employeeToUpdate);
        }

        // Loads Supervisor drop-down list
        private void PopulateSupervisorsDropDownList(object selectedSupervisor = null)
        {
            var supervisorsQuery = from s in _context.Supervisors
                                   orderby s.FullName
                                   select s;
            ViewBag.SupervisorID = new SelectList(supervisorsQuery.AsNoTracking(), "SupervisorID", "FullName", selectedSupervisor);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(s => s.Supervisor)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
    }
}
