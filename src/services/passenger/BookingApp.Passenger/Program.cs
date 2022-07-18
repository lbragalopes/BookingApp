using BookingApp.Core.ConfigExtensions;
using BookingApp.Core.Generator;
using BookingApp.Core.Maspter;
using BookingApp.Core.Options;
using BookingApp.Passenger.API;
using BookingApp.Passenger.API.Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var env = builder.Environment;
// Add services to the container.
var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<PassengerDBContext>(options =>
                    options.UseNpgsql(
                          configuration.GetConnectionString("DefaultConnection"),
                         x => x.MigrationsAssembly(typeof(PassengerDBContext).Assembly.GetName().Name)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(typeof(PassengerRoot).Assembly);
builder.Services.AddMediatR(typeof(PassengerRoot).Assembly);
builder.Services.AddCustomMapster(typeof(PassengerRoot).Assembly);
SnowFlakIdGenerator.Configure(2);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGet("/", x => x.Response.WriteAsync(appOptions.Name));

app.Run();