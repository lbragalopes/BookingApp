using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Dtos;

namespace BookingApp.Flight.API.Application.GetAvailableFlights
{
    public record GetAvailableFlightsQ : IQuery<IEnumerable<FlightResponseDto>>
    {
    }

}