using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Dtos;

namespace BookingApp.Flight.API.Application.DeleteFlight;
public record DeleteFlightCommand(long Id) : ICommand<FlightResponseDto>;


