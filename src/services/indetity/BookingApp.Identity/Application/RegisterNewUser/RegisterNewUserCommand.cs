using BookingApp.Core.CQRS;
using BookingApp.Identity.API.Dtos;

namespace BookingApp.Identity.API.Application.RegisterNewUser;
public record RegisterNewUserCommand(string FirstName, string LastName, string Username, string Email,
 string Password, string ConfirmPassword, string PassportNumber) : ICommand<RegisterNewUserResponseDto>;

