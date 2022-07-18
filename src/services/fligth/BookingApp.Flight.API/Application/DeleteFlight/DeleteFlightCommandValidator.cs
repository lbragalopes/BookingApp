using FluentValidation;

namespace BookingApp.Flight.API.Application.DeleteFlight
{
    public class DeleteFlightCommandValidator : AbstractValidator<DeleteFlightCommand>
    {
        public DeleteFlightCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
