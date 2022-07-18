using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Dtos;

namespace BookingApp.Flight.API.Application.GetFlightById
{
    public record GetFlightByIdQ(long Id) : IQuery<FlightResponseDto>;
    
    
}
