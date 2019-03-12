using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Models
{
    public class BookingForCreation
    {
        public Guid FlightId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
