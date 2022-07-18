using Microsoft.EntityFrameworkCore;

namespace BookingApp.Passenger.API.Data
{
    public class PassengerDBContext : DbContext
    {
        public PassengerDBContext(DbContextOptions<PassengerDBContext> options) : base(options)
        {
        }

        public DbSet<Passengers.Models.Passenger> Passengers => Set<Passengers.Models.Passenger>();
        public async Task<bool> Commit()
        {
            var sucess = await base.SaveChangesAsync() > 0;

            return sucess;
        }
    }
}