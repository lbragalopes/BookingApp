using BookingApp.Bus.Contracts;
using MassTransit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookingApp.Email.SendEmail
{
    public class EmailConsumer : IConsumer<SendEmailRequestDto>
    {
        private readonly IRequestClient<RequestUserByPassportNumber> _client;
        private readonly IConfiguration _config;

        public EmailConsumer(IRequestClient<RequestUserByPassportNumber> client, IConfiguration configuration)
        {
            _client = client;
            _config = configuration;
        }
        public async Task Consume(ConsumeContext<SendEmailRequestDto> context)
        {

            var apiKey = _config.GetSection("apiKey").Value;
            var client = new SendGridClient(apiKey);

            var identityResponse = await _client.GetResponse<GetUserResponse>(new RequestUserByPassportNumber(context.Message.PassengerPassport));

            var identityMessage = identityResponse.Message;

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("put_your_email_here@hotmail.com", "Sender"),
                Subject = "Reservation",
                PlainTextContent = $"Hi {context.Message.PassengerName}! Your reservation was created!"
            };

            msg.AddTo(new EmailAddress(identityMessage.PassengerEmail));

            var response = await client.SendEmailAsync(msg);

            Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
        }
    }
}