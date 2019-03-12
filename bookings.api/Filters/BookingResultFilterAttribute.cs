using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Filters
{
    public class BookingResultFilterAttribute : ResultFilterAttribute
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

            result.Value = Mapper.Map<Models.Booking>(result.Value);

            await next();
        }
    }
}
