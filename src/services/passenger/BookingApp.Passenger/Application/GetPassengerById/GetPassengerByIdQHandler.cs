using Ardalis.GuardClauses;
using BookingApp.Core.CQRS;
using BookingApp.Passenger.API.Data;
using BookingApp.Passenger.API.Dtos;
using BookingApp.Passenger.API.Exception;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Passenger.API.Application.GetPassengerById
{
    public class GetPassengerByIdQHandler : IQueryHandler<GetPassengerByIdQ, PassengerResponseDto>
    {
        private readonly PassengerDBContext passengerDBContext;
        private readonly IMapper mapper;

        public GetPassengerByIdQHandler(IMapper mapper, PassengerDBContext passengerDBContext)
        {
            this.mapper = mapper;
            this.passengerDBContext = passengerDBContext;
        }

        public async Task<PassengerResponseDto> Handle(GetPassengerByIdQ query, CancellationToken cancellationToken)
        {
            Guard.Against.Null(query, nameof(query));

            var passenger =
                await passengerDBContext.Passengers.SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if (passenger is null)
                throw new PassengerNotFound();

            return mapper.Map<PassengerResponseDto>(passenger!);
        }
    }
}