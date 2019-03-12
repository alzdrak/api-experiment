using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookings.api.Filters;
using bookings.api.Models;
using bookings.api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using booking.api;

namespace bookings.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BookingsResultFilter]
    public class BookingsCollectionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;

        public BookingsCollectionController(IMapper mapper, 
            IBookingRepository bookingRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bookingRepository = bookingRepository ?? 
                throw new ArgumentNullException(nameof(bookingRepository)); ;
        }

        [HttpGet("({bookingIds})", Name = "GetBookingsCollection")]
        public async Task<IActionResult> GetBookingsCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> bookingIds)
        {
            var bookings = await _bookingRepository.GetBookingsAsync(bookingIds);

            //check if all bookings were found
            if (bookingIds.Count() != bookings.Count())
                return NotFound();

            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookingCollection(
            [FromBody] IEnumerable<BookingForCreation> model)
        {
            if (ModelState.IsValid)
            {
                //map the model to list of bookings
                var bookingCollection = _mapper.Map<IEnumerable<Entities.Booking>>(model);

                //traverse collection and add each booking to dbset
                foreach (var booking in bookingCollection)
                {
                    //add the individual booking to dbset
                    _bookingRepository.AddBooking(booking);
                }

                //save bookings to database
                await _bookingRepository.SaveChangesAsync();

                //get all bookings that were stored
                var bookingsToReturn = await _bookingRepository
                    .GetBookingsAsync(bookingCollection.Select(b => b.BookingId).ToList());

                //build string of booking id's
                var bookingIds = string.Join(",", bookingsToReturn.Select(b => b.BookingId));

                return CreatedAtRoute("GetBookingsCollection", new { bookingIds }, bookingsToReturn);
            }

            return BadRequest();
        }
    }
}