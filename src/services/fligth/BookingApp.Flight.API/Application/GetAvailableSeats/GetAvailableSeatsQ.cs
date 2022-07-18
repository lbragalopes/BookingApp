

using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Dtos;

namespace BookingApp.Flight.API.Application.GetAvailableSeats;
public record GetAvailableSeatsQ(long FlightId) : IQuery<IEnumerable<SeatResponseDto>>;


