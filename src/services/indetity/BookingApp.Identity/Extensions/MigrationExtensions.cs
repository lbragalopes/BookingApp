using BookingApp.Core.Data;
using BookingApp.Identity.API.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Identity.API.Extensions
{
    public static class MigrationExtensions
    {
        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            MigrateDatabase(app.ApplicationServices);
            SeedData(app.ApplicationServices);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            context.Database.Migrate();
        }

        private static void SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
            foreach (var seeder in seeders)
            {
                seeder.SeedAllAsync().GetAwaiter().GetResult();
            }
        }
    }
}