using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;
using System.Net.Mail;
using System.Net;

namespace ASPFlightManager.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly FlightManagerDbContext _context;

        public ReservationsController(FlightManagerDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            var flights = _context.Flights;
            var model = new ReservationViewModel
            {
                Flights = flights,
                Reservation = new Reservation()
            };
            return View(model);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the selected flight based on its ID
                var selectedFlight = await _context.Flights.FindAsync(viewModel.Reservation.Flight.Id);

                if (selectedFlight != null)
                {
                    // Associate the selected flight with the reservation
                    viewModel.Reservation.Flight = selectedFlight;

                    _context.Add(viewModel.Reservation);
                    await _context.SaveChangesAsync();

                    SendEmail(viewModel.Reservation);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the case where the selected flight is not found
                    ModelState.AddModelError(string.Empty, "Selected flight not found.");
                }
            }
            // If ModelState is not valid or flight is not found, return the view with the viewModel to display validation errors
            viewModel.Flights = _context.Flights; // Populate flights again for the dropdown
            return View(viewModel);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,SecondName,LastName,Email,SSN,PhoneNumber,Nationality,TicketType")] Reservation reservation)
        {
            if (id != reservation.Id)
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
                    if (!ReservationExists(reservation.Id))
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
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
        public void SendEmail(Reservation reservation)
        {
            SmtpClient client = new SmtpClient("smtp.mailtrap.io");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("62fc31ef4c5f5e", "45944e2ba3e7f3");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("flightmanager@dev.local");
            mailMessage.To.Add(reservation.Email);
            mailMessage.IsBodyHtml = false;
            mailMessage.Body = "Your reservation for flight number " + reservation.Flight.PlaneNumber + " was successful." + "\n";
            mailMessage.Subject = "Reservation confirmation.";
            client.Send(mailMessage);

        }
        }

}
