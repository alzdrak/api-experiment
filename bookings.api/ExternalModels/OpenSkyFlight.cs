using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.ExternalModels
{
    public class OpenSkyFlight
    {
        public string Icao24 { get; set; }
        public string Callsign { get; set; }
        public string EstDepartureAirport { get; set; }
        public string EstArrivalAirport { get; set; }
    }
}
