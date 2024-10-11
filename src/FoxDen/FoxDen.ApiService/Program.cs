using FoxDen.Data;
using Microsoft.EntityFrameworkCore;

namespace FoxDen.ApiService
{
    /// <summary>
    /// The main entry point of this application.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add service defaults & Aspire components.
            builder.AddServiceDefaults();

            // Add services to the container.
            builder.Services.AddProblemDetails();

            builder.AddNpgsqlDbContext<FoxDenDbContext>("FoxDenData",
                settingsBuilder =>
                {
                },
                optionsBuilder => optionsBuilder.UseNpgsql(npgsqlBuilder =>
                {
                    npgsqlBuilder.MigrationsAssembly(typeof(FoxDenDbContext).Assembly.GetName().Name);
                }));

            builder.Services.AddAuthentication()
                .AddKeycloakJwtBearer
                (
                    "keycloak",
                    realm: builder.Configuration.GetValue<string>("Authentication:Keycloak:Realm")!,
                    options =>
                    {
                        options.Audience = builder.Configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidAudience");
                        options.RequireHttpsMetadata = builder.Environment.IsProduction();
                    });
            /*
            builder.Services.AddKeycloakWebApiAuthentication
            (
                builder.Configuration,
                options =>
                {
                    options.Audience = "DataService";
                }
            );
            */
            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            app.UseExceptionHandler();

            var summaries = new[]
            {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

            app.MapGet("/weatherforecast", () =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
                return forecast;
            });

            app.MapDefaultEndpoints();


            app.Run();
        }
    }

    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
