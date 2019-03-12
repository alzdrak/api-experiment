using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookings.api.Filters;
using bookings.api.Models;
using bookings.api.Services;
using AutoMapper;
using bookings.api.ExternalModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookings.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IOpenSkyFlightRepository _openSkyFlightRepository;

        public BookingsController(IMapper mapper, IBookingRepository bookingRepository, IOpenSkyFlightRepository openSkyFlightRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bookingRepository = bookingRepository ??
                throw new ArgumentNullException(nameof(bookingRepository));
            _openSkyFlightRepository = openSkyFlightRepository ?? 
                throw new ArgumentNullException(nameof(openSkyFlightRepository));
        }

        [HttpGet]
        [BookingsResultFilter]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet]
        [BookingWithFlightsResultFilter]
        [Route("{bookingId}", Name = "GetBooking")]
        public async Task<IActionResult> GetBooking(Guid bookingId)
        {
            var booking = await _bookingRepository.GetBookingAsync(bookingId);
            if (booking == null)
                return NotFound();

            //get flight details from external api
            var flights = await _openSkyFlightRepository.GetFlightAsync(
                booking.Flight.AircraftIcao24,
                new DateTime(booking.Flight.DepartureDate.Ticks + booking.Flight.DepartureTime.Ticks, DateTimeKind.Utc),
                new DateTime(booking.Flight.DestinationDate.Ticks + booking.Flight.DestinationTime.Ticks, DateTimeKind.Utc)
            );

            return Ok((booking, flights));
        }

        [HttpPost]
        [BookingResultFilter]
        public async Task<IActionResult> CreateBooking([FromBody] BookingForCreation model)
        {
            if (ModelState.IsValid) {

                //map object
                var booking = _mapper.Map<Entities.Booking>(model);

                //add to dbset
                _bookingRepository.AddBooking(booking);

                //save to database
                if (await _bookingRepository.SaveChangesAsync())
                {
                    //refecth the book
                    await _bookingRepository.GetBookingAsync(booking.BookingId);

                    return CreatedAtRoute("GetBooking",
                        new { bookingId = booking.BookingId },
                        booking);
                }
            }

            return BadRequest();
        }
    }
}