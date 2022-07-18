using BookingApp.Core.Data;
using BookingApp.Flight.Infra.Context;
using BookingApp.Fligth.Domain.Aircrafts.Models;
using BookingApp.Fligth.Domain.Airports;
using BookingApp.Fligth.Domain.Fights;
using BookingApp.Fligth.Domain.Seats;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Flight.Infra.Seed
{
    public class FlightDataSeeder : IDataSeeder
    {

        private readonly FlightDBContext flightDbContext;

        public FlightDataSeeder(FlightDBContext flightDbContext)
        {
            this.flightDbContext = flightDbContext;
        }

        public async Task SeedAllAsync()
        {
            await SeedAirportAsync();
            await SeedAircraftAsync();
            await SeedFlightAsync();
            await SeedSeatAsync();

        }


        private async Task SeedAirportAsync()
        {
            if (!await flightDbContext.Airports.AnyAsync())
            {
                var airports = new List<Airport>
            {
                Airport.Create(1, "Lisbon International Airport", "LIS", "12988"),
                Airport.Create(2, "Sao Paulo International Airport", "BRZ", "11200")
            };

                await flightDbContext.Airports.AddRangeAsync(airports);
                await flightDbContext.SaveChangesAsync();
            }


        }

        private async Task SeedAircraftAsync()
        {
            if (!await flightDbContext.Aircraft.AnyAsync())
            {
                var aircrafts = new List<Aircraft>
            {
                Aircraft.Create(1, "Boeing 737", "B737"),
                Aircraft.Create(2, "Airbus 300", "A300"),
                Aircraft.Create(3, "Airbus 320", "A320")
            };

                await flightDbContext.Aircraft.AddRangeAsync(aircrafts);
                await flightDbContext.SaveChangesAsync();
            }
        }

        private async Task SeedSeatAsync()
        {
            if (!await flightDbContext.Seats.AnyAsync())
            {
                var seats = new List<Seat>
            {
                Seat.Create(1 ,"12A", SeatType.Window, SeatClass.Economy, 1),
                Seat.Create(2, "12B", SeatType.Window, SeatClass.Economy, 1),
                Seat.Create(3, "12C", SeatType.Middle, SeatClass.Economy, 1),
                Seat.Create(4, "12D", SeatType.Middle, SeatClass.Economy, 1),
                Seat.Create(5, "12E", SeatType.Aisle, SeatClass.Economy, 1),
                Seat.Create(6, "12F", SeatType.Aisle, SeatClass.Economy, 1)
            };

                await flightDbContext.Seats.AddRangeAsync(seats);
                await flightDbContext.SaveChangesAsync();
            }
        }

        private async Task SeedFlightAsync()
        {
            if (!await flightDbContext.Flights.AnyAsync())
            {
                var flights = new List<Fligth.Domain.Flights.Flight>
            {
               Fligth.Domain.Flights.Flight.Create(1, "BD467", 1, 1, new DateTime(2022, 1, 31, 12, 0, 0),
                    new DateTime(2022, 1, 31, 14, 0, 0),
                    2, 120m,
                    new DateTime(2022, 1, 31), FlightStatus.Completed,
                    8000)
            };
                await flightDbContext.Flights.AddRangeAsync(flights);
                await flightDbContext.SaveChangesAsync();
            }
        }
    }
}