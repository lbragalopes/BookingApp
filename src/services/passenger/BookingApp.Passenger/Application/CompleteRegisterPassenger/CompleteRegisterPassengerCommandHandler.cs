using Ardalis.GuardClauses;
using BookingApp.Core.CQRS;
using BookingApp.Passenger.API.Data;
using BookingApp.Passenger.API.Dtos;
using BookingApp.Passenger.API.Exception;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Passenger.API.Application.CompleteRegisterPassenger
{
    public class CompleteRegisterPassengerCommandHandler : ICommandHandler<CompleteRegisterPassengerCommand, PassengerResponseDto>
    {
        private readonly IMapper mapper;
        private readonly PassengerDBContext passengerDBContext;

        public CompleteRegisterPassengerCommandHandler(IMapper mapper, PassengerDBContext passengerDBContext)
        {
            this.mapper = mapper;
            this.passengerDBContext = passengerDBContext;
        }

        public async Task<PassengerResponseDto> Handle(CompleteRegisterPassengerCommand command, CancellationToken cancellationToken)
        {
            Guard.Against.Null(command, nameof(command));

            var passenger = await passengerDBContext.Passengers.AsNoTracking()
                .SingleOrDefaultAsync(
                x => x.PassportNumber == command.PassportNumber, cancellationToken);

            if (passenger is null)
                throw new PassengerNotExist();

            var passengerEntity = passenger.CompleteRegistrationPassenger(passenger.Id, passenger.Name, passenger.PassportNumber, command.PassengerType, command.Age);

            var updatePassenger = passengerDBContext.Passengers.Update(passengerEntity);

            await passengerDBContext.SaveChangesAsync();

            return mapper.Map<PassengerResponseDto>(updatePassenger.Entity);
        }
    }
}