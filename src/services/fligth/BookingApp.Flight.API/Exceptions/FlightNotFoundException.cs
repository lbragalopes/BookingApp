using BookingApp.Core.Exception;

namespace BookingApp.Flight.API.Application.Exceptions
{
    public class FlightNotFoundException : NotFoundException
    {
        public FlightNotFoundException() : base("Flight not found!")
        {
        }
    }
}
