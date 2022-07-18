using AutoMapper;
using BookingApp.Booking.API.Exceptions;
using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Dtos;
using BookingApp.Flight.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Flight.API.Application.UpdateFlight
{
    public class UpdateFlightCommandHandler : ICommandHandler<UpdateFlightCommand, FlightResponseDto>
    {
        private readonly FlightDBContext flightDBContext;
        private readonly IMapper mapper;

        public UpdateFlightCommandHandler(IMapper mapper, FlightDBContext flightDbContext)
        {
            this.mapper = mapper;
            this.flightDBContext = flightDBContext;
        }

        public async Task<FlightResponseDto> Handle(UpdateFlightCommand command, CancellationToken cancellationToken)
        {

            var flight = await flightDBContext.Flights.SingleOrDefaultAsync(x => x.Id == command.Id,
                cancellationToken);

            if (flight is null)
                throw new FlightNotFound();


            flight.Update(command.Id, command.FlightNumber, command.AircraftId, command.DepartureAirportId, command.DepartureDate,
                command.ArriveDate, command.ArriveAirportId, command.DurationMinutes, command.FlightDate, command.Price, command.IsDeleted);

            var updateFlight = flightDBContext.Flights.Update(flight);

            return mapper.Map<FlightResponseDto>(updateFlight.Entity);
        }
    }
}