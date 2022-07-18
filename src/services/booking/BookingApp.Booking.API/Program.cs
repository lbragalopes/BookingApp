using BookingApp.Booking.API;
using BookingApp.Booking.Domain.Interface;
using BookingApp.Booking.Infra.Context;
using BookingApp.Booking.Infra.Repository;
using BookingApp.Bus;
using BookingApp.Core.ConfigExtensions;
using BookingApp.Core.Generator;
using BookingApp.Core.Jwt;
using BookingApp.Core.Maspter;
using BookingApp.Core.Options;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;


var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<BookingDBContext>(options =>
                    options.UseNpgsql(
                          configuration.GetConnectionString("DefaultConnection"),
                         x => x.MigrationsAssembly(typeof(BookingDBContext).Assembly.GetName().Name)));



builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwt();
SnowFlakIdGenerator.Configure(3);
builder.Services.AddValidatorsFromAssembly(typeof(BookingRoot).Assembly);
builder.Services.AddMediatR(typeof(BookingRoot).Assembly);
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomMapster(typeof(BookingRoot).Assembly);
builder.Services.AddCustomMassTransit(configuration, typeof(BookingRoot).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});

app.MapGet("/", x => x.Response.WriteAsync(appOptions.Name));
app.Run();
