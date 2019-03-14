using bookings.api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Contexts
{
    public class FlightBookingContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

        public FlightBookingContext(DbContextOptions<FlightBookingContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API - not necessary here because of simplicity
            //modelBuilder.Entity<Booking>(entity =>
            //{
            //    entity.ToTable("Booking");
            //    entity.HasKey(b => b.BookingId);
            //    entity.HasOne(b => b.Flight).WithMany(f => f.Bookings).HasForeignKey(b => b.FlightId);
            //    entity.HasOne(b => b.Passenger).WithMany(p => p.Bookings).HasForeignKey(b => b.FlightId);
            //});

            //seed - https://randomuser.me/
            modelBuilder.Entity<Passenger>().HasData(
                new Passenger()
                {
                    PassengerId = Guid.Parse("79a20e84-9884-435e-9d9b-d289f86ba417"),
                    Firstname = "Jeffrey",
                    Lastname = "Torres",
                    Age = 44,
                    Email = "jeffrey.torres64@example.com",
                    Mobile = "(928)-196-2012"
                },
                new Passenger()
                {
                    PassengerId = Guid.Parse("1a932098-0370-4daa-afb4-dc9efbf8489a"),
                    Firstname = "Sergio",
                    Lastname = "Fowler",
                    Age = 38,
                    Email = "sergio.fowler60@example.com",
                    Mobile = "(718)-504-4291"
                },
                new Passenger()
                {
                    PassengerId = Guid.Parse("6c01b522-8ccc-4d3a-ba80-bdecba3ccf11"),
                    Firstname = "Victoria",
                    Lastname = "Fisher",
                    Age = 30,
                    Email = "v.fisher88@example.com",
                    Mobile = "(322)-572-2823"
                },
                new Passenger()
                {
                    PassengerId = Guid.Parse("d40b35dd-09d5-4409-8c6e-b7e7efd3be6d"),
                    Firstname = "Doris",
                    Lastname = "Watkins",
                    Age = 41,
                    Email = "doris.watkins59@example.com",
                    Mobile = "(559)-767-4133"
                }
            );

            modelBuilder.Entity<Flight>().HasData(
                // https://opensky-network.org/api/flights/aircraft?begin=1552197600&end=1552284000&icao24=406f74
                new Flight()
                {
                    FlightId = Guid.Parse("27340d4a-4e2a-40aa-9397-4846f113ff0a"),
                    AircraftIcao24 = "406f74",
                    Callsign = "BAW39",
                    FlightNumber = "BA39",
                    DepartureAirport = "London Heathrow International Airport",
                    DepartureDate = new DateTime(2019, 3, 10, 0, 0, 0, DateTimeKind.Utc),
                    DepartureTime = new TimeSpan(16, 25, 0),
                    DestinationAirport = "Los Angeles International Airport",
                    DestinationDate = new DateTime(2019, 3, 11, 0, 0, 0, DateTimeKind.Utc),
                    DestinationTime = new TimeSpan(10, 25, 0)
                },

                // https://opensky-network.org/api/flights/aircraft?begin=1550422800&end=1550448000&icao24=4ca4e6
                new Flight()
                {
                    FlightId = Guid.Parse("d53eee6f-e184-45c7-aa14-e2f1bba458f7"),
                    AircraftIcao24 = "4ca4e6",
                    Callsign = "RYR61LP",
                    FlightNumber = "FR2693",
                    DepartureAirport = "Lisbon International Airport",
                    DepartureDate = new DateTime(2019, 2, 17, 0, 0, 0, DateTimeKind.Utc),
                    DepartureTime = new TimeSpan(18, 55, 0),
                    DestinationAirport = "Rome - Ciampino International Airport",
                    DestinationDate = new DateTime(2019, 2, 17, 0, 0, 0, DateTimeKind.Utc),
                    DestinationTime = new TimeSpan(22, 55, 0)
                },

                // https://opensky-network.org/api/flights/aircraft?begin=1517184000&end=1517270400&icao24=00b205
                new Flight()
                {
                    FlightId = Guid.Parse("79e640fb-9f34-4a68-93c7-ed331760c88b"),
                    AircraftIcao24 = "00b205",
                    Callsign = "SAA041",
                    FlightNumber = "SA41",
                    DepartureAirport = "Victoria Falls Airport",
                    DepartureDate = new DateTime(2019, 1, 29, 0, 0, 0, DateTimeKind.Utc),
                    DepartureTime = new TimeSpan(13, 25, 0),
                    DestinationAirport = "Johannesburg OR Tambo International Airport",
                    DestinationDate = new DateTime(2019, 1, 29, 0, 0, 0, DateTimeKind.Utc),
                    DestinationTime = new TimeSpan(15, 05, 0)
                }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking()
                {
                    BookingId = Guid.Parse("1b9e4b3a-998e-4455-a140-8e08d2aec327"),
                    FlightId = Guid.Parse("27340d4a-4e2a-40aa-9397-4846f113ff0a"),
                    PassengerId = Guid.Parse("79a20e84-9884-435e-9d9b-d289f86ba417")
                },

                new Booking()
                {
                    BookingId = Guid.Parse("cb407a2d-80df-41a7-882b-6466e66fc0f9"),
                    FlightId = Guid.Parse("27340d4a-4e2a-40aa-9397-4846f113ff0a"),
                    PassengerId = Guid.Parse("1a932098-0370-4daa-afb4-dc9efbf8489a")
                },
                new Booking()
                {
                    BookingId = Guid.Parse("0eb96b73-7e26-4142-a45c-212fc1f4f34a"),
                    FlightId = Guid.Parse("d53eee6f-e184-45c7-aa14-e2f1bba458f7"),
                    PassengerId = Guid.Parse("1a932098-0370-4daa-afb4-dc9efbf8489a")
                },
                new Booking()
                {
                    BookingId = Guid.Parse("0c5a45b3-2f08-4ef9-8c07-ce52bcdb1519"),
                    FlightId = Guid.Parse("79e640fb-9f34-4a68-93c7-ed331760c88b"),
                    PassengerId = Guid.Parse("6c01b522-8ccc-4d3a-ba80-bdecba3ccf11")
                },
                new Booking()
                {
                    BookingId = Guid.Parse("82966a83-94ee-4c5a-be2c-f78e35f91b90"),
                    FlightId = Guid.Parse("79e640fb-9f34-4a68-93c7-ed331760c88b"),
                    PassengerId = Guid.Parse("d40b35dd-09d5-4409-8c6e-b7e7efd3be6d")
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
