using BookingApp.Core.Exception;

namespace BookingApp.Flight.API.Application.Exceptions
{
    public class FlightAlreadyExist : ConflictException
    {
        public FlightAlreadyExist(int? code = default) : base("Flight already exist!", code)
        {
        }
    }
}
