using AutoMapper;
using BookingApp.Bus.Contracts;
using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Application.Exceptions;
using BookingApp.Flight.API.Dtos;
using BookingApp.Flight.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Flight.API.Application.CreateFlight
{
    public class CreateFlightCommandHandler : ICommandHandler<CreateFlightCommand, FlightResponseDto>
    {
        private readonly FlightDBContext flightDBContext;
        private readonly IMapper mapper;

        public CreateFlightCommandHandler(IMapper mapper,
            FlightDBContext flightDbContext)

        {
            this.mapper = mapper;
            this.flightDBContext = flightDbContext;

        }

        public async Task<FlightResponseDto> Handle(CreateFlightCommand command, CancellationToken cancellationToken)
        {

            var flight = await flightDBContext.Flights.SingleOrDefaultAsync(x => x.Id == command.Id,
                cancellationToken);

            if (flight is not null)
                throw new FlightAlreadyExist();

            var flightEntity = BookingApp.Fligth.Domain.Flights.Flight.Create(command.Id, command.FlightNumber, command.AircraftId,
                command.DepartureAirportId, command.DepartureDate,
                command.ArriveDate, command.ArriveAirportId, command.DurationMinutes, command.FlightDate,
                command.Price);
                       
            var newFlight = await flightDBContext.Flights.AddAsync(flightEntity, cancellationToken);



            return mapper.Map<FlightResponseDto>(newFlight.Entity);
        }

             
    }
}
