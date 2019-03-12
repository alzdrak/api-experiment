using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Entities
{
    [Table("Passenger")]
    public class Passenger
    {
        [Key]
        public Guid PassengerId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(150)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(150)]
        public int Age { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Mobile { get; set; }

        //passport, country, etc...

        public virtual List<Booking> Bookings { get; set; }
    }
}
