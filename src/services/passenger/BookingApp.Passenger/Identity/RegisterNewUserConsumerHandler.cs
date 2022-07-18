using BookingApp.Bus.Contracts;
using BookingApp.Core.Generator;
using BookingApp.Passenger.API.Data;
using MassTransit;

namespace BookingApp.Passenger.API.Identity
{
    public class RegisterNewUserConsumerHandler : IConsumer<UserCreated>
    {

        private readonly PassengerDBContext passengerDBContext;

        public RegisterNewUserConsumerHandler(PassengerDBContext passengerDBContext)
        {
            this.passengerDBContext = passengerDBContext;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var passenger = Passengers.Models.Passenger.Create(SnowFlakIdGenerator.NewId(), context.Message.Name, context.Message.PassportNumber);

            await passengerDBContext.AddAsync(passenger);

            await passengerDBContext.SaveChangesAsync();
        }
    }
}