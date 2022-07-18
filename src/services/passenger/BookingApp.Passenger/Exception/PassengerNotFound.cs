
using BookingApp.Core.Exception;

namespace BookingApp.Passenger.API.Exception;

    public class PassengerNotFound : NotFoundException
    {
        public PassengerNotFound(string code = default) : base("Passenger not found!")
        {
        }
    }