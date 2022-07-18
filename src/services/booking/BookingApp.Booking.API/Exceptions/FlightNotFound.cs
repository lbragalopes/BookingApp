using BookingApp.Core.Exception;

namespace BookingApp.Booking.API.Exceptions;

    public class FlightNotFound : NotFoundException
    {
        public FlightNotFound() : base("Flight doesn't exist!")
        {
        }
    }
