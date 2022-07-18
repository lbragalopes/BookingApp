using BookingApp.Identity.API.Data.Context;
using BookingApp.Identity.API.User.Models;
using Microsoft.AspNetCore.Identity;

namespace BookingApp.Identity.API.Extensions
{
    public static class IdentityServerExtensions
    {
        public static IServiceCollection AddIdentityServer(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<long>>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            var identityServerBuilder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
               .AddAspNetIdentity<ApplicationUser>()
                .AddResourceOwnerValidator<UserValidator>();

            if (env.IsDevelopment())
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }

            return services;
        }

    }
}