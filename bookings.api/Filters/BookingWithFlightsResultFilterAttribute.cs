using AutoMapper;
using bookings.api.Entities;
using bookings.api.ExternalModels;
using bookings.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Filters
{
    public class BookingWithFlightsResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;
            if (result?.Value == null ||
                result.StatusCode < 200 ||
                result.StatusCode >= 300)
            {
                await next();
                return;
            }

            var (booking, flights) = ((Entities.Booking, IEnumerable<OpenSkyFlight>))result.Value;

            var mappedBooking = Mapper.Map<BookingWithFlights>(booking);

            result.Value = Mapper.Map(flights, mappedBooking);

            await next();
        }
    }
}
