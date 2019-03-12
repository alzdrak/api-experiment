using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookings.api.Profiles
{
    public class BookingsProfile : Profile
    {
        public BookingsProfile()
        {
            CreateMap<Entities.Booking, Models.Booking>()
                .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.Flight.FlightNumber))
                .ForMember(dest => dest.FlightDetails, opt => opt.MapFrom(src =>
                    $"{src.Flight.DepartureAirport} - {src.Flight.DestinationAirport}"))
                .ForMember(dest => dest.FlightDate, opt => opt.MapFrom(src =>
                    $"{src.Flight.DepartureDate.ToString("yyyy/MM/dd")} {src.Flight.DepartureTime}"))
                .ForMember(dest => dest.PassengerName, opt => opt.MapFrom(src => 
                    $"{src.Passenger.Firstname} {src.Passenger.Lastname}"));

            CreateMap<Models.BookingForCreation, Entities.Booking>()
                .ForPath(dest => dest.Passenger.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForPath(dest => dest.Passenger.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForPath(dest => dest.Passenger.Age, opt => opt.MapFrom(src => src.Age))
                .ForPath(dest => dest.Passenger.Mobile, opt => opt.MapFrom(src => src.Mobile))
                .ForPath(dest => dest.Passenger.Email, opt => opt.MapFrom(src => src.Email));


            CreateMap<Entities.Booking, Models.BookingWithFlights>()
                .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.Flight.FlightNumber))
                .ForMember(dest => dest.FlightDetails, opt => opt.MapFrom(src =>
                    $"{src.Flight.DepartureAirport} - {src.Flight.DestinationAirport}"))
                .ForMember(dest => dest.FlightDate, opt => opt.MapFrom(src =>
                    $"{src.Flight.DepartureDate.ToString("yyyy/MM/dd")} {src.Flight.DepartureTime}"))
                .ForMember(dest => dest.PassengerName, opt => opt.MapFrom(src =>
                    $"{src.Passenger.Firstname} {src.Passenger.Lastname}"));

            CreateMap<IEnumerable<ExternalModels.OpenSkyFlight>, Models.BookingWithFlights>()
                .ForMember(dest => dest.Flights, opt => opt.MapFrom(src => src));
        }
    }
}
