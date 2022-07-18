using Ardalis.GuardClauses;
using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Dtos;
using BookingApp.Flight.API.Exceptions;
using BookingApp.Flight.Infra.Context;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Flight.API.Application.ReserveSeat
{
    public class ReserveSeatCommandHandler : ICommandHandler<ReserveSeatCommand, SeatResponseDto>
    {
        private readonly FlightDBContext flightDBContext;
        private readonly IMapper mapper;

        public ReserveSeatCommandHandler(IMapper mapper, FlightDBContext flightDBContext)
        {
            this.mapper = mapper;
            this.flightDBContext = flightDBContext;
        }

        public async Task<SeatResponseDto> Handle(ReserveSeatCommand command, CancellationToken cancellationToken)
        {
            Guard.Against.Null(command, nameof(command));

            var seat = await flightDBContext.Seats.SingleOrDefaultAsync(x => x.SeatNumber == command.SeatNumber && x.FlightId == command.FlightId, cancellationToken);

            if (seat is null)
                throw new SeatNumberIncorrect();

            var reserveSeat = await seat.ReserveSeat(seat);

            var updatedSeat = flightDBContext.Seats.Update(reserveSeat);
           
            flightDBContext.SaveChanges();

            return mapper.Map<SeatResponseDto>(updatedSeat.Entity);
        }
    }
}