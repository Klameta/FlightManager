using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPFlightManager.Views.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly FlightManagerDbContext _context;
        public IndexModel(FlightManagerDbContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string EmailSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Reservation> Reservations { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            EmailSort = sortOrder == "Email" ? "email_desc" : "Email";

            IQueryable<Reservation> reservationIQ = from s in _context.Reservations
                                             select s;

            switch (sortOrder)
            {
                case "name_desc":
                    reservationIQ = reservationIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Email":
                    reservationIQ = reservationIQ.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    reservationIQ = reservationIQ.OrderByDescending(s => s.Email);
                    break;
                default:
                    reservationIQ = reservationIQ.OrderBy(s => s.LastName);
                    break;
            }

            Reservations = await reservationIQ.AsNoTracking().ToListAsync();
        }
    }
}
