using FoxDen.Web;
using FoxDen.Web.Authentication;
using FoxDen.Web.Components;
using FoxDen.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add service defaults & Aspire components.
        builder.AddServiceDefaults();
        builder.AddRedisOutputCache("cache");

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddHttpClient<WeatherApiClient>(client =>
            {
                // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
                // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
                client.BaseAddress = new("https+http://apiservice");
            });

        builder.Services.AddNpgsql<AuthDbContext>
        (
            builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found."),
            null,
            options => options.UseNpgsql(builder => builder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name))
        );
        builder.Services.AddDefaultIdentity<IdentityUser<Guid>>().AddEntityFrameworkStores<AuthDbContext>();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication()
            .AddCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(builder.Configuration.GetValue("SessionCookieLifetimeMinutes", 60)))
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
            })
            .AddDiscord(discordOptions =>
            {
                discordOptions.ClientId = builder.Configuration["Authentication:Discord:ClientId"]!;
                discordOptions.ClientSecret = builder.Configuration["Authentication:Discord:ClientSecret"]!;

                discordOptions.ClaimActions.MapCustomJson("urn:discord:avatar:url", user
                    => string.Format
                    (
                        CultureInfo.InvariantCulture,
                        "https://cdn.discordapp.com/avatars/{0}/{1}.{2}",
                        user.GetString("id"),
                        user.GetString("avatar"),
                        user.GetString("avatar").StartsWith("a_") ? "gif" : "png"
                    ));
            });

        builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<LogOutService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.UseOutputCache();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapDefaultEndpoints();

        app.Run();
    }
}
