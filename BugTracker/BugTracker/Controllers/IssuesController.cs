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
    public class IssuesController : Controller
    {
        private readonly BugTrackerDbContext _context;

        public IssuesController(BugTrackerDbContext context)
        {
            _context = context;
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
            var bugTrackerDbContext = _context.Issues.Include(i => i.IssueClosedByNavigation).Include(i => i.IssueCreatedByNavigation).Include(i => i.IssuePriorityNavigation).Include(i => i.IssueTypeNavigation).Include(i => i.ProjectNavigation);
            return View(await bugTrackerDbContext.ToListAsync());
        }

        public async Task<IActionResult> Active()
        {
            var isInProgress = _context.Issues.Where(i => i.IssueType == "Active");
            return View(await isInProgress.ToListAsync());
        }
        public async Task<IActionResult> Finish()
        {
            var isInProgress = _context.Issues.Where(i => i.IssueType == "Completed");
            return View(await isInProgress.ToListAsync());
        }

        public async Task<IActionResult> Abandon()
        {
            var isInProgress = _context.Issues.Where(i => i.IssueType == "Abandoned");
            return View(await isInProgress.ToListAsync());
        }

        public async Task<IActionResult> UnderReview()
        {
            var isInProgress = _context.Issues.Where(i => i.IssueType == "Completed");
            return View(await isInProgress.ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.IssueClosedByNavigation)
                .Include(i => i.IssueCreatedByNavigation)
                .Include(i => i.IssuePriorityNavigation)
                .Include(i => i.IssueTypeNavigation)
                .Include(i => i.ProjectNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            ViewData["IssueClosedBy"] = new SelectList(_context.UserProfiles, "DisplayName", "DisplayName");
            ViewData["IssueCreatedBy"] = new SelectList(_context.UserProfiles, "DisplayName", "DisplayName");
            ViewData["IssuePriority"] = new SelectList(_context.IssuePriorities, "IssuePriorityType", "IssuePriorityType");
            ViewData["IssueType"] = new SelectList(_context.IssueStatuses, "IssueStatusType", "IssueStatusType");
            ViewData["Project"] = new SelectList(_context.Projects, "Name", "Name");
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IssueTitle,IssueDescription,IssueType,IssuePriority,IssueCreatedBy,IssueCreatedOn,IssueClosedBy,IssueClosedOn,IssueResolutionSummary,Project")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IssueClosedBy"] = new SelectList(_context.UserProfiles, "DisplayName", "DisplayName", issue.IssueClosedBy);
            ViewData["IssueCreatedBy"] = new SelectList(_context.UserProfiles, "DisplayName", "DisplayName", issue.IssueCreatedBy);
            ViewData["IssuePriority"] = new SelectList(_context.IssuePriorities, "IssuePriorityType", "IssuePriorityType", issue.IssuePriority);
            ViewData["IssueType"] = new SelectList(_context.IssueStatuses, "IssueStatusType", "IssueStatusType", issue.IssueType);
            ViewData["Project"] = new SelectList(_context.Projects, "Name", "Name", issue.Project);
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Complete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            ViewData["IssueClosedBy"] = new SelectList(_context.UserProfiles, "DisplayName", "DisplayName", issue.IssueClosedBy);
            ViewData["IssueCreatedBy"] = new SelectList(_context.UserProfiles, "DisplayName", "DisplayName", issue.IssueCreatedBy);
            ViewData["IssuePriority"] = new SelectList(_context.IssuePriorities, "IssuePriorityType", "IssuePriorityType", issue.IssuePriority);
            ViewData["IssueType"] = new SelectList(_context.IssueStatuses, "IssueStatusType", "IssueStatusType", issue.IssueType);
            ViewData["Project"] = new SelectList(_context.Projects, "Name", "Name", issue.Project);
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id, [Bind("Id,IssueTitle,IssueDescription,IssueType,IssuePriority,IssueCreatedBy,IssueCreatedOn,IssueClosedBy,IssueClosedOn,IssueResolutionSummary,Project")] Issue issue)
        {
            issue.IssueType = "Completed";
            issue.IssueClosedOn = DateTime.Now; 
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
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
            ViewData["IssueClosedBy"] = new SelectList(_context.UserProfiles, "DisplayName", "DisplayName", issue.IssueClosedBy);
            ViewData["IssueCreatedBy"] = new SelectList(_context.UserProfiles, "DisplayName", "DisplayName", issue.IssueCreatedBy);
            ViewData["IssuePriority"] = new SelectList(_context.IssuePriorities, "IssuePriorityType", "IssuePriorityType", issue.IssuePriority);
            ViewData["IssueType"] = new SelectList(_context.IssueStatuses, "IssueStatusType", "IssueStatusType", issue.IssueType);
            ViewData["Project"] = new SelectList(_context.Projects, "Name", "Name", issue.Project);
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.IssueClosedByNavigation)
                .Include(i => i.IssueCreatedByNavigation)
                .Include(i => i.IssuePriorityNavigation)
                .Include(i => i.IssueTypeNavigation)
                .Include(i => i.ProjectNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }
    }
}
