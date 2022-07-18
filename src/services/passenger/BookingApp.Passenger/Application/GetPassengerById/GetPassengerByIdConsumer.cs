using BookingApp.Bus.Contracts;
using Mapster;
using MassTransit;
using MediatR;

namespace BookingApp.Passenger.API.Application.GetPassengerById
{
    public class GetPassengerByIdConsumer : IConsumer<GetPassengerByIdRequest>
    {

        private IMediator mediator;

        public GetPassengerByIdConsumer(IMediator mediator)
        {
            this.mediator = mediator;

        }

        public async Task Consume(ConsumeContext<GetPassengerByIdRequest> context)
        {
            var query = new GetPassengerByIdQ(context.Message.PassengerId);

            var passengerResponseDto = await mediator.Send(query);

            var result = passengerResponseDto.Adapt<PassengerResponse>();

            await context.RespondAsync(result);

        }
    }
}