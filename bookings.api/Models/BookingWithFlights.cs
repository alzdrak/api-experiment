using bookings.api.Models;
using bookings.api.ExternalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Models
{
    public class BookingWithFlights : Booking
    {
        public IEnumerable<OpenSkyFlight> Flights { get; set; } = new List<OpenSkyFlight>();
    }
}
