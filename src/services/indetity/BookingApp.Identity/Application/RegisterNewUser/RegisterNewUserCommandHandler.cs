using BookingApp.Bus.Contracts;
using BookingApp.Core.CQRS;
using BookingApp.Identity.API.Dtos;
using BookingApp.Identity.API.Exceptions;
using BookingApp.Identity.API.User;
using BookingApp.Identity.API.User.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace BookingApp.Identity.API.Application.RegisterNewUser
{
    public class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand, RegisterNewUserResponseDto>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPublishEndpoint publishEndpoint;

        public RegisterNewUserCommandHandler(UserManager<ApplicationUser> userManager, IPublishEndpoint publishEndpoint)
        {
            this.userManager = userManager;
            this.publishEndpoint = publishEndpoint;

        }

        public async Task<RegisterNewUserResponseDto> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                UserName = command.Username,
                Email = command.Email,
                PasswordHash = command.Password,
                PassPortNumber = command.PassportNumber
            };

            var identityResult = await userManager.CreateAsync(applicationUser, command.Password);
            var roleResult = await userManager.AddToRoleAsync(applicationUser, Constants.Role.User);

            if (identityResult.Succeeded == false)
                throw new RegisterIdentityUser(string.Join(',', identityResult.Errors.Select(e => e.Description)));

            if (roleResult.Succeeded == false)
                throw new RegisterIdentityUser(string.Join(',', roleResult.Errors.Select(e => e.Description)));

            await publishEndpoint.Publish<UserCreated>(new UserCreated(applicationUser.Id, applicationUser.FirstName + " " + applicationUser.LastName,
                 applicationUser.PassPortNumber));


            return new RegisterNewUserResponseDto
            {
                Id = applicationUser.Id,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Username = applicationUser.UserName,
                PassportNumber = applicationUser.PassPortNumber
            };

        }
    }
}