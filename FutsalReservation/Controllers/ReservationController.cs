using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FutsalReservation.Data;
using FutsalReservation.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FutsalReservation.Controllers
{
    public class ReservationController : Controller
    {
        private readonly FutsalReservationContext _context;
        private List<String> _timings = new List<String> { "5am - 6am", "6am - 7am", "7am - 8am", "8am - 9am", "9am - 10am", "10am - 11am", "11am - 12pm", "12pm - 1pm", "1pm - 2pm", "2pm - 3pm", "3pm - 4pm", "4pm - 5pm", "5pm - 6pm", "6pm - 7pm", "7pm - 8pm", "8pm - 9pm", "9pm - 10pm" };
        private readonly DateTime _date = DateTime.Now;
        public List<String> availableTimings = new List<String>();
        public ReservationController(FutsalReservationContext context)
        {
            _context = context;
        }

        // GET: Reservation
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var currentUserName = User.Claims.First(c => c.Type == "UserName").Value;
            var currentUser = _context.User.First(u => u.Username == currentUserName);
            ViewData["UserId"] = currentUser.Id;
            var futsalReservationContext = _context.Reservation.Include(r => r.Court).Include(r => r.User);
            return View(await futsalReservationContext.ToListAsync());
        }

        // GET: Reservation/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Court)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        [Authorize(Roles = "Admin, Normal")]
        public IActionResult UserCreate(int? id)
        {
            var user = _context.User.Where(u => u.Id == id);
            var today = $"{_date.Year}/{_date.Month}/{_date.Day}";
            AvailableTimings(today);
            ViewData["Today"] = $"{_date.Year}/{_date.Month}/{_date.Day}";
            ViewData["Timings"] = new SelectList(availableTimings);
            ViewData["CourtId"] = new SelectList(_context.Court, "Id", "Name");
            ViewData["UserId"] = new SelectList(user, "Id", "Email");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var today = $"{_date.Year}/{_date.Month}/{_date.Day}";
            AvailableTimings(today);
            ViewData["Today"] = $"{_date.Year}/{_date.Month}/{_date.Day}";
            ViewData["Timings"] = new SelectList(_timings);
            ViewData["CourtId"] = new SelectList(_context.Court, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email");
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ReservationId,UserId,CourtId,ReservationDate,ReservationTime")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourtId"] = new SelectList(_context.Court, "Id", "Name", reservation.CourtId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", reservation.UserId);
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Normal")]
        public async Task<IActionResult> UserCreate([Bind("ReservationId,UserId,CourtId,ReservationDate,ReservationTime")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                var userName = User.Claims.First(c => c.Type == "UserName").Value;
                var user = _context.User.First(u => u.Username == userName);
                return RedirectToAction("ShowReservations", "User", user);
            }
            ViewData["CourtId"] = new SelectList(_context.Court, "Id", "Name", reservation.CourtId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", reservation.UserId);
            return View(reservation);
        }
        // GET: Reservation/Edit/5
        [Authorize(Roles = "Admin, Normal")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            var today = $"{_date.Year}/{_date.Month}/{_date.Day}";
            AvailableTimings(today);
            ViewData["Today"] = $"{_date.Year}/{_date.Month}/{_date.Day}";
            ViewData["Timings"] = new SelectList(availableTimings);
            ViewData["CourtId"] = new SelectList(_context.Court, "Id", "Name", reservation.CourtId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", reservation.UserId);
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Normal")]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,UserId,CourtId,ReservationDate,ReservationTime")] Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
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
            ViewData["CourtId"] = new SelectList(_context.Court, "Id", "Name", reservation.CourtId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        [Authorize(Roles = "Admin, Normal")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Court)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Normal")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservation == null)
            {
                return Problem("Entity set 'FutsalReservationContext.Reservation'  is null.");
            }
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
          return _context.Reservation.Any(e => e.ReservationId == id);
        }

        public void AvailableTimings(string? date)
        {
            var reservationsForToday = _context.Reservation.Where(r => r.ReservationDate == date).ToList();
            if (reservationsForToday.Count == 0)
            {
                availableTimings = _timings.ToList();
            }
            else
            {
                List<String> reservedTimings = new List<String>();
                foreach (var reservation in reservationsForToday)
                {
                    reservedTimings.Add(reservation.ReservationTime);
                }
                availableTimings = _timings.Except(reservedTimings).ToList();
            }
        }
    }
}
