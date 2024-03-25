using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Flight
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Type { get; set; }
        public string PlaneNumber { get; set; }
        public string PilotName { get; set; }
        public string Capacity { get; set; }
        public string CapacityVIP { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
