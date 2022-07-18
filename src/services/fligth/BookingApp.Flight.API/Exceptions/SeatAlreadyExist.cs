using BookingApp.Core.Exception;

namespace BookingApp.Flight.API.Exceptions
{
    public class SeatAlreadyExist : ConflictException
    {
        public SeatAlreadyExist(int? code = default) : base("Seat already exist!", code)
        {
        }
    }
}