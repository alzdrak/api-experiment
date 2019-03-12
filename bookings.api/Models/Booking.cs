using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Models
{
    public class Booking
    {
        public Guid BookingId { get; set; }
        public string FlightNumber { get; set; }
        public string FlightDetails { get; set; }
        public string FlightDate { get; set; }
        public string PassengerName { get; set; }
    }
}
