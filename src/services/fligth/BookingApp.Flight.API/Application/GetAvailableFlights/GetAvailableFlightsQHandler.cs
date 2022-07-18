using Ardalis.GuardClauses;
using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Application.Exceptions;
using BookingApp.Flight.API.Dtos;
using BookingApp.Flight.Infra.Context;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Flight.API.Application.GetAvailableFlights
{
    public class GetAvailableFlightsQHandler : IQueryHandler<GetAvailableFlightsQ, IEnumerable<FlightResponseDto>>
    {
        private readonly IMapper mapper;
        private readonly FlightDBContext flightDBContext;

        public GetAvailableFlightsQHandler(IMapper mapper, FlightDBContext flightDBContext)
        {
            this.mapper = mapper;
            this.flightDBContext = flightDBContext;
        }

        public async Task<IEnumerable<FlightResponseDto>> Handle(GetAvailableFlightsQ query,
            CancellationToken cancellationToken)
        {
            Guard.Against.Null(query, nameof(query));

            var flight = (await flightDBContext.Flights.AsQueryable().ToListAsync(cancellationToken))
                .Where(x => !x.IsDeleted);

            if (!flight.Any())
                throw new FlightNotFoundException();

            return mapper.Map<List<FlightResponseDto>>(flight);
        }
    }

}