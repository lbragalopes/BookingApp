using Ardalis.GuardClauses;
using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Application.Exceptions;
using BookingApp.Flight.API.Dtos;
using BookingApp.Flight.Infra.Context;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Flight.API.Application.GetFlightById
{
    public class GetFlightIdQHandler : IQueryHandler<GetFlightByIdQ, FlightResponseDto>
    {
        private readonly IMapper mapper;
        private readonly FlightDBContext flightDBContext;

        public GetFlightIdQHandler(IMapper mapper, FlightDBContext flightDBContext)
        {
            this.mapper = mapper;
            this.flightDBContext = flightDBContext;
        }

        public async Task<FlightResponseDto> Handle(GetFlightByIdQ query, CancellationToken cancellationToken)
        {
            Guard.Against.Null(query, nameof(query));

            var flight =
                await flightDBContext.Flights.AsQueryable().SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if (flight is null)
                throw new FlightNotFoundException();

            return mapper.Map<FlightResponseDto>(flight);
        }
    }
}