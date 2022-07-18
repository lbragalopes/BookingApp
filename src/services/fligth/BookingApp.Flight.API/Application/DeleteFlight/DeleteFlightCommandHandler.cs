using AutoMapper;
using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Application.Exceptions;
using BookingApp.Flight.API.Dtos;
using BookingApp.Flight.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Flight.API.Application.DeleteFlight
{
    public class DeleteFlightCommandHandler : ICommandHandler<DeleteFlightCommand, FlightResponseDto>
    {
        private readonly FlightDBContext flightDBContext;
        private readonly IMapper mapper;

        public DeleteFlightCommandHandler(IMapper mapper, FlightDBContext flightDBContext)
        {
            this.mapper = mapper;
            this.flightDBContext = flightDBContext;
        }

        public async Task<FlightResponseDto> Handle(DeleteFlightCommand command, CancellationToken cancellationToken)
        {
            var flight = await flightDBContext.Flights.SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (flight is null)
                throw new FlightNotFoundException();


            var deleteFlight = flightDBContext.Flights.Remove(flight).Entity;

            flight.Delete(deleteFlight.Id, deleteFlight.FlightNumber, deleteFlight.AircraftId, deleteFlight.DepartureAirportId,
                deleteFlight.DepartureDate, deleteFlight.ArriveDate, deleteFlight.ArriveAirportId, deleteFlight.DurationMinutes,
                deleteFlight.FlightDate, deleteFlight.Status, deleteFlight.Price);

            return mapper.Map<FlightResponseDto>(deleteFlight);
        }
    }
}