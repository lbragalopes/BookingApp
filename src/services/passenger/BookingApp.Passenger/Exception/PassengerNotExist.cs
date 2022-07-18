using BookingApp.Core.Exception;

namespace BookingApp.Passenger.API.Exception;

    public class PassengerNotExist : BadRequestException
    {
        public PassengerNotExist(string code = default) : base("Please register before!")
        {
        }
    }

