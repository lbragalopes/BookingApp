using BookingApp.Core.Exception;

namespace BookingApp.Identity.API.Exceptions;

    public class RegisterIdentityUser : AppException
    {
        public RegisterIdentityUser(string error) : base(error)
        {
        }
    }
