﻿using BookingApp.Bus.Contracts;
using FluentValidation;

namespace BookingApp.Flight.API.Application.CreateFlight
{
    public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
    {
        public CreateFlightCommandValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Status).Must(p => (p.GetType().IsEnum &&
                                             p == FlightStatus.Flying) ||
                                             p == FlightStatus.Canceled ||
                                             p == FlightStatus.Delay ||
                                             p == FlightStatus.Completed)
                .WithMessage("Status must be Flying, Delay, Canceled or Completed");

            RuleFor(x => x.AircraftId).NotEmpty().WithMessage("AircraftId must be not empty");
            RuleFor(x => x.DepartureAirportId).NotEmpty().WithMessage("DepartureAirportId must be not empty");
            RuleFor(x => x.ArriveAirportId).NotEmpty().WithMessage("ArriveAirportId must be not empty");
            RuleFor(x => x.DurationMinutes).GreaterThan(0).WithMessage("DurationMinutes must be greater than 0");
            RuleFor(x => x.FlightDate).NotEmpty().WithMessage("FlightDate must be not empty");
        }
    }
}