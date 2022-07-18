using BookingApp.Booking.Domain.Interface;
using BookingApp.Booking.Infra.Context;
using BookingApp.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Booking.Infra.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDBContext context;


        public BookingRepository(BookingDBContext context)
        {
            this.context = context;
        }

        public IUnitOfWork UnitOfWork => context;

        public DbConnection GetConnection() => context.Database.GetDbConnection();

        public void Add(Domain.Models.Booking booking)
        {
            context.Bookings.AddAsync(booking);
        }

        public async Task<Domain.Models.Booking> GetById(Guid id)
        {
            return await context.Bookings.FindAsync(id);
        }

        public void Update(Domain.Models.Booking booking)
        {
            context.Bookings.Update(booking);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}