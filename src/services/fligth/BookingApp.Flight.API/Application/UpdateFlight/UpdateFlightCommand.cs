﻿using BookingApp.Bus.Contracts;
using BookingApp.Core.CQRS;
using BookingApp.Flight.API.Dtos;

namespace BookingApp.Flight.API.Application.UpdateFlight
{
    public class UpdateFlightCommand : ICommand<FlightResponseDto>
    {
        public long Id { get; init; }
        public string FlightNumber { get; init; }
        public long AircraftId { get; init; }
        public long DepartureAirportId { get; init; }
        public DateTime DepartureDate { get; init; }
        public DateTime ArriveDate { get; init; }
        public long ArriveAirportId { get; init; }
        public decimal DurationMinutes { get; init; }
        public DateTime FlightDate { get; init; }

        public FlightStatus Status { get; init; }

        public bool IsDeleted { get; init; } = false;
        public decimal Price { get; init; }
        public string CacheKey => "GetAvailableFlightsQuery";
    }
}