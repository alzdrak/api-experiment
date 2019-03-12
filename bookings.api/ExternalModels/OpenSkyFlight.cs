using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.ExternalModels
{
    public class OpenSkyFlight
    {
        public string Icao24 { get; set; }
        public string callsign { get; set; }
        public string estDepartureAirport { get; set; }
        public string estArrivalAirport { get; set; }
    }
}
