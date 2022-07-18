using BookingApp.Core.Exception;

namespace BookingApp.Flight.API.Exceptions
{
    public class SeatNumberIncorrect : BadRequestException
    {
        public SeatNumberIncorrect() : base("Seat number is incorrect!")
        {
        }
    }

}
