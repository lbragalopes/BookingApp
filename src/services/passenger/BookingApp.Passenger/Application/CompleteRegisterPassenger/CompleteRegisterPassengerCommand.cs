using BookingApp.Core.CQRS;
using BookingApp.Core.Generator;
using BookingApp.Passenger.API.Dtos;
using BookingApp.Passenger.API.Passengers.Models;

namespace BookingApp.Passenger.API.Application.CompleteRegisterPassenger
{
    public record CompleteRegisterPassengerCommand(string PassportNumber, PassengerType PassengerType, int Age) : ICommand<PassengerResponseDto>
    {
        public long Id { get; set; } = SnowFlakIdGenerator.NewId();
    }
  
}

