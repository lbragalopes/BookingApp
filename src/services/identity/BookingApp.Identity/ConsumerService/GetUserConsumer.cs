using BookingApp.Bus.Contracts;
using BookingApp.Identity.API.Data.Context;
using MassTransit;

namespace BookingApp.Identity.API.ConsumerService
{
    public class GetUserConsumer : IConsumer<RequestUserByPassportNumber>
    {
        private readonly IdentityContext context;

        public GetUserConsumer(IdentityContext context)
        {
            this.context = context;
        }
        public async Task Consume(ConsumeContext<RequestUserByPassportNumber> request)
        {
            var userEmail = context.Users.Where(x => x.PassPortNumber.Equals(request.Message.passportNumber)).ToList();

            var email = userEmail.Single().Email;
            if (userEmail is null)
            {
                throw new NotImplementedException("Email não encontrado");
            }

            await request.RespondAsync<GetUserResponse>(new GetUserResponse { PassengerEmail = email });
        }
    }
}