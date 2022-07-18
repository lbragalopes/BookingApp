using BookingApp.Core.CQRS;
using BookingApp.Passenger.API.Dtos;

namespace BookingApp.Passenger.API.Application.GetPassengerById;
public record GetPassengerByIdQ(long Id): IQuery<PassengerResponseDto>;

