using bookings.api.Contexts;
using bookings.api.ExternalModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace bookings.api.Services
{
    /// <summary>
    /// <para>
    /// Connects to external API that provides air traffic.
    /// </para>
    /// <para>
    /// You can find more info here:
    /// https://opensky-network.org/apidoc/rest.html
    /// </para>
    /// </summary>
    public class OpenSkyFlightRepository : IOpenSkyFlightRepository, IDisposable
    {
        private FlightBookingContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OpenSkyFlightRepository> _logger;
        
        public OpenSkyFlightRepository(FlightBookingContext context, 
            IHttpClientFactory httpClientFactory,
            ILogger<OpenSkyFlightRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpClientFactory = httpClientFactory ?? 
                throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<OpenSkyFlight>> GetFlightAsync(string icao24, DateTime start, DateTime end)
        {
            var httpClient = _httpClientFactory.CreateClient();

            //api uses unix time in seconds
            long startUnix = ((DateTimeOffset)start).ToUnixTimeSeconds();
            long endUnix = ((DateTimeOffset)end).ToUnixTimeSeconds();

            //call to external api, should probably store this in appsettings.
            var response = await httpClient
                .GetAsync($"https://opensky-network.org/api/flights/aircraft?begin={startUnix}&end={endUnix}&icao24={icao24}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<OpenSkyFlight>>
                    (await response.Content.ReadAsStringAsync());
            }

            return null;
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

    public interface IOpenSkyFlightRepository
    {
        /// <summary>
        /// Get flight info from external api
        /// </summary>
        /// <param name="icao24">Aircraft route identifier</param>
        /// <param name="start">start time in unix ticks</param>
        /// <param name="end">end time in unix ticks</param>
        /// <returns>Info about the aircraft route</returns>
        Task<IEnumerable<OpenSkyFlight>> GetFlightAsync(string icao24, DateTime start, DateTime end);
    }
}
