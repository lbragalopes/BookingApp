using BookingApp.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookingApp.Booking.Infra.Context
{
   public class BookingDBContext: DbContext, IUnitOfWork
    {
        public BookingDBContext(DbContextOptions<BookingDBContext> options) : base(options)
        {
           
        }

        public DbSet<Domain.Models.Booking> Bookings => Set<Domain.Models.Booking>();


        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;
            return success;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}