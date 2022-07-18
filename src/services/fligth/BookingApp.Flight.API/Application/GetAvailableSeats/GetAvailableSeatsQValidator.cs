using FluentValidation;

namespace BookingApp.Flight.API.Application.GetAvailableSeats
{
    public class GetAvailableSeatsQValidator : AbstractValidator<GetAvailableSeatsQ>
    {
        public GetAvailableSeatsQValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FlightId).NotNull().WithMessage("FlightId is required!");
        }
    }
}