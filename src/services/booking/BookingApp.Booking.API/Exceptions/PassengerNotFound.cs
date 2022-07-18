using BookingApp.Core.Exception;

namespace BookingApp.Booking.API.Exceptions;

public class PassengerNotFound : NotFoundException
{
    public PassengerNotFound() : base("Flight doesn't exist! ")
    {
    }
}

