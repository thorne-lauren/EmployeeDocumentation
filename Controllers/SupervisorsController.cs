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
    public class SupervisorsController : Controller
    {
        private readonly TeamContext _context;

        public SupervisorsController(TeamContext context)
        {
            _context = context;
        }

        // GET: Supervisors
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            var supervisors = from s in _context.Supervisors
                           select s;
            switch (sortOrder)
            {
                case "lastname_desc":
                    supervisors = supervisors.OrderByDescending(s => s.LastName);
                    break;
                default:
                    supervisors = supervisors.OrderBy(s => s.LastName);
                    break;
            }
            return View(await supervisors.AsNoTracking().ToListAsync());
        }

        // GET: Supervisors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisors
                .Include(e => e.Employees)
                    .ThenInclude(e => e.Documentation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SupervisorID == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        // GET: Supervisors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supervisors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupervisorID,LastName,FirstName,Extension")] Supervisor supervisor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(supervisor);
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
            return View(supervisor);
        }

        // GET: Supervisors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisors.FindAsync(id);
            if (supervisor == null)
            {
                return NotFound();
            }
            return View(supervisor);
        }

        // POST: Supervisors/Edit/5
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
            var supervisorToUpdate = await _context.Supervisors.FirstOrDefaultAsync(s => s.SupervisorID == id);
            if (await TryUpdateModelAsync<Supervisor>(
                supervisorToUpdate,
                "",
                s => s.LastName, s => s.FirstName, s => s.Extension))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(supervisorToUpdate);
        }
        // GET: Supervisors/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisors
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SupervisorID == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(supervisor);
        }

        // POST: Supervisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supervisor = await _context.Supervisors.FindAsync(id);
            if (supervisor == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Supervisors.Remove(supervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool SupervisorExists(int id)
        {
            return _context.Supervisors.Any(e => e.SupervisorID == id);
        }
    }
}
