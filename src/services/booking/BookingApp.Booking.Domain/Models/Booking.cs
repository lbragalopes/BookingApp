﻿using BookingApp.Booking.Domain.Models.ValueObjects;
using BookingApp.Core.ModelsAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Booking.Domain.Models
{
    public class Booking : Aggregate<Guid>
    {
        public Booking()
        {
        }
        public Trip Trip { get; private set; }
        public PassengerInfo PassengerInfo { get; private set; }

        public static Booking Create(Guid id, PassengerInfo passengerInfo, Trip trip, bool isDeleted = false)
        {
            var booking = new Booking()
            {
                Id = id,
                Trip = trip,
                PassengerInfo = passengerInfo,
                IsDeleted = isDeleted
            };
            return booking;
        }
    }
}