using BookingApp.Core.Exception;

namespace BookingApp.Flight.API.Exceptions
{
    public class AllSeatsFullException : BadRequestException
    {
        public AllSeatsFullException() : base("All seats are full!")
        {
        }
    }
}
   