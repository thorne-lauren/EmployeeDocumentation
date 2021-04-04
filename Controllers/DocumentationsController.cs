using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeDocumentation.Data;
using EmployeeDocumentation.Models;
using EmployeeDocumentation.Models.TeamViewModels;

namespace EmployeeDocumentation.Controllers
{
    public class DocumentationsController : Controller
    {
        private readonly TeamContext _context;

        public DocumentationsController(TeamContext context)
        {
            _context = context;
        }

        // GET: Documentations
        public async Task<IActionResult> Index()
        {
            var documentation = _context.Documentations
                .Include(e => e.Employee)
                .AsNoTracking();
            return View(await documentation.ToListAsync());
        }

        // GET: Documentation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentations
                .Include(d => d.Employee)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DocumentationID == id);
            if (documentation == null)
            {
                return NotFound();
            }

            return View(documentation);
        }

        // GET: Documentation/Create
        public IActionResult Create()
        {
            PopulateEmployeesDropDownList();
            return View();
        }

        // POST: Documentation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentationID,EmployeeID,AuthorInitials,Category,Date,Entry")] Documentation documentation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(documentation);
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
            PopulateEmployeesDropDownList(documentation.EmployeeID);
            return View(documentation);
        }

        // GET: Documentation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentations
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DocumentationID == id);
            if (documentation == null)
            {
                return NotFound();
            }
            PopulateEmployeesDropDownList(documentation.EmployeeID);
            return View(documentation);
        }

        // POST: Documentation/Edit/5
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

            var documentationToUpdate = await _context.Documentations
                .FirstOrDefaultAsync(d => d.DocumentationID == id);

            if (await TryUpdateModelAsync<Documentation>(documentationToUpdate,
                "",
                d => d.EmployeeID, d => d.AuthorInitials, d => d.Category, d => d.Date, d => d.Entry))
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
            PopulateEmployeesDropDownList(documentationToUpdate.EmployeeID);
            return View(documentationToUpdate);
        }

        // Loads employee dropdown
        private void PopulateEmployeesDropDownList(object selectedEmployee = null)
        {
            var employeesQuery = from e in _context.Employees
                                   orderby e.FullName
                                   select e;
            ViewBag.EmployeeID = new SelectList(employeesQuery.AsNoTracking(), "EmployeeID", "FullName", selectedEmployee);
        }

        // GET: Documentation/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentations
                .Include(d => d.Employee)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DocumentationID == id);
            if (documentation == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(documentation);
        }

        // POST: Documentation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentation = await _context.Documentations.FindAsync(id);
            if (documentation == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Documentations.Remove(documentation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool DocumentationExists(int id)
        {
            return _context.Documentations.Any(e => e.DocumentationID == id);
        }
    }
}
