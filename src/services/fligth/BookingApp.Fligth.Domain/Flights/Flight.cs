using BookingApp.Core.ModelsAggregate;
using BookingApp.Fligth.Domain.Fights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Fligth.Domain.Flights
{
    public class Flight : Aggregate<long>
    {
        public string FlightNumber { get; private set; }
        public long AircraftId { get; private set; }
        public DateTime DepartureDate { get; private set; }
        public long DepartureAirportId { get; private set; }
        public DateTime ArriveDate { get; private set; }
        public long ArriveAirportId { get; private set; }
        public decimal DurationMinutes { get; private set; }
        public DateTime FlightDate { get; private set; }
        public FlightStatus Status { get; private set; }
        public decimal Price { get; private set; }


        public static Flight Create(long id, string flightNumber, long aircraftId,
            long departureAirportId, DateTime departureDate, DateTime arriveDate,
            long arriveAirportId, decimal durationMinutes, DateTime flightDate, FlightStatus status,
            decimal price, bool isDeleted = false)
        {
            var flight = new Flight
            {
                Id = id,
                FlightNumber = flightNumber,
                AircraftId = aircraftId,
                DepartureAirportId = departureAirportId,
                DepartureDate = departureDate,
                ArriveDate = arriveDate,
                ArriveAirportId = arriveAirportId,
                DurationMinutes = durationMinutes,
                FlightDate = flightDate,
                Status = status,
                Price = price,
                IsDeleted = isDeleted,
            };
            return flight;
        }

        public void Update(long id, string flightNumber, long aircraftId,
        long departureAirportId, DateTime departureDate, DateTime arriveDate,
        long arriveAirportId, decimal durationMinutes, DateTime flightDate, FlightStatus status,
        decimal price, bool isDeleted = false)
        {
            FlightNumber = flightNumber;
            AircraftId = aircraftId;
            DepartureAirportId = departureAirportId;
            DepartureDate = departureDate;
            arriveDate = ArriveDate;
            ArriveAirportId = arriveAirportId;
            DurationMinutes = durationMinutes;
            FlightDate = flightDate;
            Status = status;
            Price = price;
            IsDeleted = isDeleted;
        }

        public void Delete(long id, string flightNumber, long aircraftId,
        long departureAirportId, DateTime departureDate, DateTime arriveDate,
        long arriveAirportId, decimal durationMinutes, DateTime flightDate, FlightStatus status,
        decimal price, bool isDeleted = true)
        {
            FlightNumber = flightNumber;
            AircraftId = aircraftId;
            DepartureAirportId = departureAirportId;
            DepartureDate = departureDate;
            arriveDate = ArriveDate;
            ArriveAirportId = arriveAirportId;
            DurationMinutes = durationMinutes;
            FlightDate = flightDate;
            Status = status;
            Price = price;
            IsDeleted = isDeleted;
        }
    }
}