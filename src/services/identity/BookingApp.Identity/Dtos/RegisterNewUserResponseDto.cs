namespace BookingApp.Identity.API.Dtos
{
    public class RegisterNewUserResponseDto
    {
        public long Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Username { get; init; }
        public string? PassportNumber { get; set; }
    }
}
