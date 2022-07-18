using FluentValidation;

namespace BookingApp.Flight.API.Application.GetFlightById
{
    public class GetFlightByIdQValidator : AbstractValidator<GetFlightByIdQ>
    {
        public GetFlightByIdQValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id).NotNull().WithMessage("Id is required!");
        }
    }
}