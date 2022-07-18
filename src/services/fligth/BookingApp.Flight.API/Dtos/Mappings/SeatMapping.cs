using BookingApp.Fligth.Domain.Seats;
using Mapster;

namespace BookingApp.Flight.API.Dtos.Mappings
{
    public class SeatMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Seat, SeatResponseDto>();
        }
    }
}