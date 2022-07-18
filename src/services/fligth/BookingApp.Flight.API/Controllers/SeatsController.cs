using BookingApp.Flight.API.Application.GetAvailableFlights;
using BookingApp.Flight.API.Application.GetAvailableSeats;
using BookingApp.Flight.API.Application.GetFlightById;
using BookingApp.Flight.API.Application.ReserveSeat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Flight.API.Controllers
{
    [Route("api/seats]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly IMediator mediator;
        public SeatsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("{FlightId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> GetAvailableSeats([FromRoute] GetAvailableSeatsQ query, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ReserveSeat([FromBody] ReserveSeatCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
