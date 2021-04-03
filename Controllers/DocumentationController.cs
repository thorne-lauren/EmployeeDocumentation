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
    public class DocumentationController : Controller
    {
        private readonly TeamContext _context;

        public DocumentationController(TeamContext context)
        {
            _context = context;
        }

        // GET: Documentation
        public async Task<IActionResult> Index()
        {
            var documentation = _context.Documentation
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

            var documentation = await _context.Documentation
                .Include(d => d.Employee)
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "LastName");
            return View();
        }

        // POST: Documentation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentationID,EmployeeID,AuthorInitials,Category,Date,Entry")] Documentation documentation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "LastName", documentation.EmployeeID);
            return View(documentation);
        }

        // GET: Documentation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentation.FindAsync(id);
            if (documentation == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "LastName", documentation.EmployeeID);
            return View(documentation);
        }

        // POST: Documentation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentationID,EmployeeID,AuthorInitials,Category,Date,Entry")] Documentation documentation)
        {
            if (id != documentation.DocumentationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentationExists(documentation.DocumentationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "LastName", documentation.EmployeeID);
            return View(documentation);
        }

        // GET: Documentation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentation = await _context.Documentation
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.DocumentationID == id);
            if (documentation == null)
            {
                return NotFound();
            }

            return View(documentation);
        }

        // POST: Documentation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentation = await _context.Documentation.FindAsync(id);
            _context.Documentation.Remove(documentation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentationExists(int id)
        {
            return _context.Documentation.Any(e => e.DocumentationID == id);
        }
    }
}
