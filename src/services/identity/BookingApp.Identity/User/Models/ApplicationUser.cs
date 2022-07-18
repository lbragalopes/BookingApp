using Microsoft.AspNetCore.Identity;

namespace BookingApp.Identity.API.User.Models
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PassPortNumber { get; set; }
    }
}
