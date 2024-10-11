using Microsoft.EntityFrameworkCore;

namespace FoxDen.Data.Manager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();

            

            // Add services to the container.
            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.MapDefaultEndpoints();
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.Run();
        }
    }
}
