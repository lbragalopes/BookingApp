using BookingApp.Core.Exception;

namespace BookingApp.Booking.API.Exceptions;

public class BookingAlreadyExist : ConflictException
{
    public BookingAlreadyExist(int? code = default) : base("Booking already exist!", code)
    {
    }
}
