using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Dtos;

namespace BookingApp.Flight.API.Application.ReserveSeat;
public record ReserveSeatCommand(long FlightId, string SeatNumber) : ICommand<SeatResponseDto>;

