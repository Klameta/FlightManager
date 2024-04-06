using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPFlightManager.Controllers
{
    public class ReservationViewModel
    {
        public DbSet<Flight> Flights { get; set; }
        public Reservation Reservation { get; set; }
    }
}