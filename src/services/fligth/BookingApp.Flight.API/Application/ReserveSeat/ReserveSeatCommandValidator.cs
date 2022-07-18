using FluentValidation;

namespace BookingApp.Flight.API.Application.ReserveSeat
{
    public class ReserveSeatCommandValidator : AbstractValidator<ReserveSeatCommand>
    {
        public ReserveSeatCommandValidator()
        {
           CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FlightId).NotEmpty().WithMessage("FlightId não deve ser vazio!");
            RuleFor(x => x.SeatNumber).NotEmpty().WithMessage("SeatNumber não deve ser vazio!");
        }
    }
}