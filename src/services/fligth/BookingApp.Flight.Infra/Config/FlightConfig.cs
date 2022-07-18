using BookingApp.Fligth.Domain.Aircrafts.Models;
using BookingApp.Fligth.Domain.Airports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Flight.Infra.Config
{
    public class FlightConfiguration : IEntityTypeConfiguration<Fligth.Domain.Flights.Flight>
    {
        public void Configure(EntityTypeBuilder<Fligth.Domain.Flights.Flight> builder)
        {
            builder.ToTable("Flight");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();

            builder
                .HasOne<Aircraft>()
                .WithMany()
                .HasForeignKey(p => p.AircraftId);

            builder
                .HasOne<Airport>()
                .WithMany()
                .HasForeignKey(d => d.DepartureAirportId)
                .HasForeignKey(a => a.ArriveAirportId);


        }
    }
}
