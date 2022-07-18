using AutoMapper;
using BookingApp.Bus.Contracts;
using BookingApp.Core.Contracts;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeatResponse = BookingApp.Core.Contracts.SeatResponse;

namespace BookingApp.Flight.API.Application.GetAvailableSeats
{
    public class GetAvailableSeatsConsumer : IConsumer<GetAvailabeSeatsbyId>
    {
        private readonly IMapper mapper;
        private IMediator mediator;

        public GetAvailableSeatsConsumer(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        public async Task Consume(ConsumeContext<GetAvailabeSeatsbyId> context)
        {
            var flighId = context.Message.FlightId;
            var query = new GetAvailableSeatsQ(flighId);
            var seatList = await mediator.Send(query);

            var message = seatList.First(); 

            await context.RespondAsync<SeatResponse>(message);
        }
    }
}