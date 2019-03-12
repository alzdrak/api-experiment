using bookings.api.Contexts;
using bookings.api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Services
{
    public class BookingRepository : IBookingRepository, IDisposable
    {
        private FlightBookingContext _context;

        public BookingRepository(FlightBookingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Passenger)
                .Include(b => b.Flight)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync(IEnumerable<Guid> bookingIds)
        {
            return await _context.Bookings
                .Where(b => bookingIds.Contains(b.BookingId))
                .Include(b => b.Passenger)
                .Include(b => b.Flight)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingAsync(Guid bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Passenger)
                .Include(b => b.Flight)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public void AddBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            _context.Add(booking);  //not using special value generator (so no async)
        }

        public async Task<bool> SaveChangesAsync()
        {
            //true if entities were changed
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        } 
    }

    public interface IBookingRepository
    {
        /// <summary>
        /// Get all bookings in the system
        /// </summary>
        /// <returns>List of Bookings</returns>
        Task<IEnumerable<Booking>> GetBookingsAsync();

        /// <summary>
        /// Get bookings for provided ids
        /// </summary>
        /// <param name="bookIds">List of booking ids</param>
        /// <returns>List of booking objects</returns>
        Task<IEnumerable<Booking>> GetBookingsAsync(IEnumerable<Guid> bookingIds);

        /// <summary>
        /// Get a specific booking
        /// </summary>
        /// <param name="bookingId">booking identifier</param>
        /// <returns>Single booking entity</returns>
        Task<Booking> GetBookingAsync(Guid bookingId);

        /// <summary>
        /// Add new booking to the dbset
        /// </summary>
        /// <param name="booking"></param>
        void AddBooking(Entities.Booking booking);

        /// <summary>
        /// Save Changes to Database
        /// </summary>
        /// <returns>True it saved</returns>
        Task<bool> SaveChangesAsync();
    }
}
