using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Eventshuffle.Application;
using Eventshuffle.Infrastructure;
using Eventshuffle.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Eventshuffle.Application.Services;
using Eventshuffle.Infrastructure.Data;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Register Application services
        services.AddScoped<IEventService, EventService>();

        // Register Infrastructure services
        services.AddScoped<IEventRepository, EventRepository>();

        // Configure Entity Framework Core with SQLite
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=app.db"));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
    }
}
