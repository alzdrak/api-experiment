using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Entities
{
    [Table("Flight")]
    public class Flight
    {
        [Key]
        public Guid FlightId { get; set; }

        [Required]
        [MaxLength(20)]
        public string AircraftIcao24 { get; set; }  //unique ICAO 24-bit aircraft code

        [Required]
        [MaxLength(20)]
        public string Callsign { get; set; }  //used for air control traffic eg BAW2491

        [Required]
        [MaxLength(20)]
        public string FlightNumber { get; set; } //flight number for operational use eg BA2491

        [Required]
        [MaxLength(255)]
        public string DepartureAirport { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [Column(TypeName = "Time")]
        public TimeSpan DepartureTime { get; set; }

        [Required]
        [MaxLength(255)]
        public string DestinationAirport { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime DestinationDate { get; set; }

        [Required]
        [Column(TypeName = "Time")]
        public TimeSpan DestinationTime { get; set; }

        //normalize to schedule, airport tables...

        public virtual List<Booking> Bookings { get; set; }
    }
}
