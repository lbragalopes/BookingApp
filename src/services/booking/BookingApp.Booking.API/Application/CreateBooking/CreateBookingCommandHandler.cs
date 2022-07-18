using BookingApp.Booking.API.Dtos;
using BookingApp.Booking.API.Exceptions;
using BookingApp.Booking.Domain.Interface;
using BookingApp.Booking.Domain.Models.ValueObjects;
using BookingApp.Bus.Contracts;
using BookingApp.Core.CQRS;
using Mapster;
using MassTransit;


namespace BookingApp.Booking.API.Application.CreateBooking
{
    public class CreateBookingCommandHandler : ICommandHandler<CreateBookingCommand, CreateReservationResponseDto>
    {
        private readonly IRequestClient<GetFlightById> clientA;
        private readonly IRequestClient<GetAvailabeSeatsById> clientB;
        private readonly IRequestClient<GetPassengerByIdRequest> clientC;
        private readonly ISendEndpointProvider sendEndpointProvider;
        private readonly IBookingRepository repository;

        public CreateBookingCommandHandler(
            IRequestClient<GetFlightById> clientA,
            IRequestClient<GetAvailabeSeatsById> clientB,
            IRequestClient<GetPassengerByIdRequest> clientC,
            ISendEndpointProvider sendEndpointProvider,
            IBookingRepository repository)
        {

            this.clientA = clientA;
            this.clientB = clientB;
            this.clientC = clientC;
            this.sendEndpointProvider = sendEndpointProvider;
            this.repository = repository;
        }


        public async Task<CreateReservationResponseDto> Handle(CreateBookingCommand command, 
            CancellationToken cancellationToken)
        {
            
            //receive the flight
            var flightMessage = await clientA.GetResponse<FlightResponse>(new { FlightId = command.FlightId }, cancellationToken);
            var flight = flightMessage.Message;
            if (flightMessage is null)
                throw new FlightNotFound();

            //recover available seats
            var emptySeatMessage = await clientB.GetResponse<SeatResponse>(new { FlightId = command.FlightId }, cancellationToken);
            var emptySeat = emptySeatMessage.Message;

            //passenger information
            var passengerMessage = await clientC.GetResponse<PassengerResponse>(new { PassengerId = command.PassengerId }, cancellationToken);
            var passenger = passengerMessage.Message;

            //check the reservation
            var reservation = await repository.GetById(command.Id);
             if (reservation is not null && !reservation.IsDeleted)
                throw new BookingAlreadyExist();


            //data persistence
            var aggregate = Domain.Models.Booking.Create(command.Id, new PassengerInfo(passenger.Name), new Trip(
             flight.FlightNumber, flight.AircraftId, flight.DepartureAirportId,
             flight.ArriveAirportId, flight.FlightDate, flight.Price, command.Description, emptySeat?.SeatNumber));

            var _serviceAddress = "queue:ReserveSeat";
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri(_serviceAddress));

            await endpoint.Send(new ReserveSeatRequestDto
            {
                FlightId = flight.Id,
                SeatNumber = emptySeat?.SeatNumber
            });

            repository.Add(aggregate);

            var result = await repository.UnitOfWork.Commit();

            var reservationResponseDto = aggregate.Adapt<CreateReservationResponseDto>();

            if (result is true)
            {
                var _serveceAddressEmail = "queue:SendEmail";
                var endpointEmail = await sendEndpointProvider.GetSendEndpoint(new Uri(_serveceAddressEmail));

                await endpointEmail.Send(new SendEmailRequestDto
                {
                    PassengerName = passenger.Name,
                    PassengerPassport = passenger.PassportNumber,
                    FlightNumber = reservationResponseDto.FlightNumber,
                    FlightDate = reservationResponseDto.FlightDate,
                    SeatNumber = reservationResponseDto.SeatNumber,

                });
            }


            return reservationResponseDto;
        }
    }
}