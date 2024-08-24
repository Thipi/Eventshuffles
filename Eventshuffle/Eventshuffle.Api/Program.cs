using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Eventshuffle.Application;
using Eventshuffle.Domain.Interfaces;
using Eventshuffle.Infrastructure.Data;
using Eventshuffle.Application.Services;
using Eventshuffle.Infrastructure;
using System;
using Eventshuffle.Utils;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
         options.JsonSerializerOptions.Converters.Add(new InputDateTimeConverter());
     });

void AddJsonOptions(Action<object> value)
{
    throw new NotImplementedException();
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Application services
builder.Services.AddScoped<IEventService, EventService>();

// Register Infrastructure services
builder.Services.AddScoped<IEventRepository, EventRepository>();

// Configure Entity Framework Core with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Build the application
var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controller routes
app.MapControllers();

app.Run();
