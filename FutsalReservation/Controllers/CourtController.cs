using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FutsalReservation.Data;
using FutsalReservation.Data.Entities;

namespace FutsalReservation.Controllers
{
    public class CourtController : Controller
    {
        private readonly FutsalReservationContext _context;
        private List<int> _hourTimings = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

        public CourtController(FutsalReservationContext context)
        {
            _context = context;
        }

        // GET: Court
        public async Task<IActionResult> Index()
        {
            var courtReservations = _context.Court.Include(r => r.Reservations).Include(t => t.Timings);
            //return View(await _context.Court.ToListAsync());
            return View(await courtReservations.ToListAsync());
        }

        // GET: Court/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Court == null)
            {
                return NotFound();
            }

            var court = await _context.Court
                .FirstOrDefaultAsync(m => m.Id == id);
            if (court == null)
            {
                return NotFound();
            }

            return View(court);
        }

        // GET: Court/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Court/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Court court)
        {
            if (ModelState.IsValid)
            {
                _context.Add(court);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(court);
        }

        // GET: Court/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Court == null)
            {
                return NotFound();
            }

            var court = await _context.Court.FindAsync(id);
            if (court == null)
            {
                return NotFound();
            }
            ViewData["HourTimings"] = new SelectList(_hourTimings);
            return View(court);
        }

        // POST: Court/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Court court,string endTime,string startTime)
        {
           Timing timing = new Timing(startTime, endTime);
            if (id != court.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var toUpdateCourt = await _context.Court.FindAsync(id);
                    if (toUpdateCourt != null)
                    {
                        toUpdateCourt.Timings = new List<Timing>();
                        toUpdateCourt.Timings.Add(timing);
                        toUpdateCourt.Name = court.Name;
                        await _context.SaveChangesAsync();
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourtExists(court.Id))
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
            return View(court);
        }

        // GET: Court/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Court == null)
            {
                return NotFound();
            }

            var court = await _context.Court
                .FirstOrDefaultAsync(m => m.Id == id);
            if (court == null)
            {
                return NotFound();
            }

            return View(court);
        }

        // POST: Court/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Court == null)
            {
                return Problem("Entity set 'FutsalReservationContext.Court'  is null.");
            }
            var court = await _context.Court.FindAsync(id);
            if (court != null)
            {
                _context.Court.Remove(court);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourtExists(int id)
        {
          return _context.Court.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ShowReservations(int? id)
        {
            if (id == null || _context.Court == null)
            {
                return NotFound();
            }
            var court = _context.Court.First(c => c.Id == id);
            var courtReservations = await _context.Reservation.Include(r => r.User).Where(r => r.CourtId == id).ToListAsync();
            if (courtReservations.Count == 0)
            {
                ViewData["Message"] = "No reservations for " + court.Name;
            }
            ViewData["UserId"] = id;
            return View(courtReservations);
        }
    }
}
