using BookingApp.Bus.Contracts;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Flight.API.Application.ReserveSeat
{
    public class ReserveSeatConsumer : IConsumer<ReserveSeatRequestDto>
    {

        private IMediator mediator;

        public ReserveSeatConsumer(IMediator mediator)
        {
            this.mediator = mediator;

        }

        public async Task Consume(ConsumeContext<ReserveSeatRequestDto> context)
        {
            var command = new ReserveSeatCommand(context.Message.FlightId, context.Message.SeatNumber);

            var result = await mediator.Send(command);
        }
    }
}