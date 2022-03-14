#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class IssuePrioritiesController : Controller
    {
        private readonly BugTrackerDbContext _context;

        public IssuePrioritiesController(BugTrackerDbContext context)
        {
            _context = context;
        }

        // GET: IssuePriorities
        public async Task<IActionResult> Index()
        {
            return View(await _context.IssuePriorities.ToListAsync());
        }

        // GET: IssuePriorities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issuePriority = await _context.IssuePriorities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issuePriority == null)
            {
                return NotFound();
            }

            return View(issuePriority);
        }

        // GET: IssuePriorities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IssuePriorities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IssuePriorityType")] IssuePriority issuePriority)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issuePriority);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issuePriority);
        }

        // GET: IssuePriorities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issuePriority = await _context.IssuePriorities.FindAsync(id);
            if (issuePriority == null)
            {
                return NotFound();
            }
            return View(issuePriority);
        }

        // POST: IssuePriorities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IssuePriorityType")] IssuePriority issuePriority)
        {
            if (id != issuePriority.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issuePriority);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssuePriorityExists(issuePriority.Id))
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
            return View(issuePriority);
        }

        // GET: IssuePriorities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issuePriority = await _context.IssuePriorities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issuePriority == null)
            {
                return NotFound();
            }

            return View(issuePriority);
        }

        // POST: IssuePriorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issuePriority = await _context.IssuePriorities.FindAsync(id);
            _context.IssuePriorities.Remove(issuePriority);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssuePriorityExists(int id)
        {
            return _context.IssuePriorities.Any(e => e.Id == id);
        }
    }
}
