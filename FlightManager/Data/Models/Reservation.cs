using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SSN { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string TicketType { get; set; }

        public virtual Flight Flight { get; set; }

    }
}
