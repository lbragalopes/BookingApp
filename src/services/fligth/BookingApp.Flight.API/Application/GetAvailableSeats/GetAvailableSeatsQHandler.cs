using Ardalis.GuardClauses;
using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Dtos;
using BookingApp.Flight.API.Exceptions;
using BookingApp.Flight.Infra.Context;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Flight.API.Application.GetAvailableSeats
{
    public class GetAvailableSeatsQHandler : IQueryHandler<GetAvailableSeatsQ, IEnumerable<SeatResponseDto>>
    {
        private readonly IMapper mapper;
        private readonly FlightDBContext flightDBContext;

        public GetAvailableSeatsQHandler(IMapper mapper, FlightDBContext flightReadDbContext)
        {
            this.mapper = mapper;
            this.flightDBContext = flightReadDbContext;
        }


        public async Task<IEnumerable<SeatResponseDto>> Handle(GetAvailableSeatsQ query, CancellationToken cancellationToken)
        {
            Guard.Against.Null(query, nameof(query));

            var seats = (await flightDBContext.Seats.AsQueryable().ToListAsync(cancellationToken))
                .Where(x => !x.IsDeleted);

            if (!seats.Any())
                throw new AllSeatsFullException();

            return mapper.Map<List<SeatResponseDto>>(seats);
        }
    }
}