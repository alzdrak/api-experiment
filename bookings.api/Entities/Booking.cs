using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Entities
{
    [Table("Booking")]
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; }
        [Required]
        public Guid FlightId { get; set; }
        [Required]
        public Guid PassengerId { get; set; }


        [ForeignKey("FlightId")]
        public Flight Flight { get; set; }
        [ForeignKey("PassengerId")]
        public Passenger Passenger { get; set; }
    }
}
