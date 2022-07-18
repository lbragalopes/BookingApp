

using BookingApp.Bus;
using BookingApp.Core.ConfigExtensions;
using BookingApp.Core.Data;
using BookingApp.Core.Generator;
using BookingApp.Core.Jwt;
using BookingApp.Core.Maspter;
using BookingApp.Core.Options;
using BookingApp.Flight.API;
using BookingApp.Flight.API.Extension;
using BookingApp.Flight.Infra.Context;
using BookingApp.Flight.Infra.Seed;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;

var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<FlightDBContext>(options =>
                    options.UseNpgsql(
                          configuration.GetConnectionString("DefaultConnection"),
                         x => x.MigrationsAssembly(typeof(FlightDBContext).Assembly.GetName().Name)));


builder.Services.AddScoped<IDataSeeder, FlightDataSeeder>();

builder.Services.AddJwt();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(FlightRoot).Assembly);
builder.Services.AddCustomMapster(typeof(FlightRoot).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(FlightRoot).Assembly);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCustomMassTransit(configuration, typeof(FlightRoot).Assembly);

SnowFlakIdGenerator.Configure(1);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseMigrations();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});

app.MapGet("/", x => x.Response.WriteAsync(appOptions.Name));

app.Run();