using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.CodeFixes;
using Project_Final.Models;

namespace Project_Final.Controllers
{
    [Authorize]
    public class VisitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: Visits
        //public async Task<IActionResult> Index()
        //{
        //    var visits = await _context.Visits
        // .Include(p => p.doctor)
        // .Include(p => p.patient)
        // .ToListAsync();

        //    return View(visits);
        //}
        public async Task<IActionResult> Index(string SearchBy, string search)
        {
            var visits = await _context.Visits
                .Include(p => p.doctor)
                .Include(p => p.patient)
                .ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                if (SearchBy == "DateOfVisit")
                {
                    visits = visits.Where(x => x.DateOfVisit.ToString().Contains(search)).ToList();
                }
                else if (SearchBy == "Complaint")
                {
                    visits = visits.Where(x => x.Complaint.Contains(search)).ToList();
                }
            }

            return View(visits);
        }


        //public IActionResult Index(string SearchBy, string search)
        //{
        //    if (SearchBy == "DateOfVisit")
        //    {

        //        return View(_context.Visits.Where(x => x.DateOfVisit.Contains(search) || search == null).ToList());
        //    }
        //    else
        //    {

        //        return View(_context.Visits.Where(x => x.Complaint.Contains(search) || search == null).ToList());

        //    }
        //}
        // GET: Visits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visits = await _context.Visits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visits == null)
            {
                return NotFound();
            }

            return View(visits);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DoctorId,PatientId,DateOfVisit,Complaint")] Visits visits)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visits);
        }

        // GET: Visits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visits = await _context.Visits.FindAsync(id);
            if (visits == null)
            {
                return NotFound();
            }
            return View(visits);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DoctorId,PatientId,DateOfVisit,Complaint")] Visits visits)
        {
            if (id != visits.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitsExists(visits.Id))
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
            return View(visits);
        }

        // GET: Visits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visits = await _context.Visits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visits == null)
            {
                return NotFound();
            }

            return View(visits);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visits = await _context.Visits.FindAsync(id);
            if (visits != null)
            {
                _context.Visits.Remove(visits);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitsExists(int id)
        {
            return _context.Visits.Any(e => e.Id == id);
        }
    }
}
