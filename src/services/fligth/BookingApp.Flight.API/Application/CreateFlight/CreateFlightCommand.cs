using BookingApp.Bus.Contracts;
using BookingApp.Core.CQRS;
using BookingApp.Core.Generator;
using BookingApp.Flight.API.Dtos;

namespace BookingApp.Flight.API.Application.CreateFlight
{
    public record CreateFlightCommand(string FlightNumber, long AircraftId, long DepartureAirportId,
     DateTime DepartureDate, DateTime ArriveDate, long ArriveAirportId,
     decimal DurationMinutes, DateTime FlightDate, FlightStatus Status, decimal Price) : ICommand<FlightResponseDto>
    {
        public long Id { get; set; } = SnowFlakIdGenerator.NewId();
    }

}
