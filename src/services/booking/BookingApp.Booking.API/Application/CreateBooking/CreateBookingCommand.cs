using BookingApp.Booking.API.Dtos;
using BookingApp.Core.CQRS;

namespace BookingApp.Booking.API.Application.CreateBooking
{
    public record CreateBookingCommand(long PassengerId, long FlightId, string Description) : ICommand<CreateReservationResponseDto>
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}
