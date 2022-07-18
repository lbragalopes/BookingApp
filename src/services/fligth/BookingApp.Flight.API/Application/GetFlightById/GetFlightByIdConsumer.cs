using AutoMapper;
using BookingApp.Bus.Contracts;
using BookingApp.Flight.Infra.Context;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Flight.API.Application.GetFlightById
{
    public class GetFlightByIdConsumer : IConsumer<Core.Contracts.GetFlightById>
    {
        private readonly IMapper mapper;
        private FlightDBContext context;
        private IMediator mediator;

        public GetFlightByIdConsumer(FlightDBContext context, IMediator mediator, IMapper mapper)
        {
            this.context = context;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<Core.Contracts.GetFlightById> context)
        {
            var query = new GetFlightByIdQ(context.Message.FlightId);
            var flight = await mediator.Send(query);

            var result = flight.Adapt<FlightResponse>();

            await context.RespondAsync<FlightResponse>(result);
        }
    }
}
