using FoxDen.Commander.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FoxDen.Commander;

public class Program
{  
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddHttpClient();
        builder.Services.AddFluentUIComponents();

        builder.Services.AddHttpClient<WeatherApiClient>(client =>
        {
            // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
            // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
            client.BaseAddress = new("https+http://dataden");
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Handle local auth
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme; // Handle external auth challenges.
        })
            .AddCookie() // Manage local sessions
            .AddKeycloakOpenIdConnect
            (
                serviceName: "keycloak",
                realm: builder.Configuration.GetValue<string>("Authentication:Keycloak:Realm")!,
                options =>
                {
                    options.ClientId = builder.Configuration.GetValue<string>("Authentication:Schemes:OpenIdConnect:ClientId");
                    options.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Schemes:OpenIdConnect:ClientSecret");
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.RequireHttpsMetadata = builder.Environment.IsProduction();
                    options.SaveTokens = true;
                    options.MapInboundClaims = false;
                    options.Scope.Clear();
                    foreach (var scope in builder.Configuration.GetValue<string[]>("Authentication:Schemes:OpenIdConnect:Scope") ?? [])
                    {
                        options.Scope.Add(scope);
                        int[] arr = [1, 2, 3];
                    }
                }
            );

        // builder.Services.AddKeycloakWebAppAuthentication(builder.Configuration);
        builder.Services.AddAuthorizationBuilder();
        // TODO: Policy stuff

        builder.Services.AddCascadingAuthenticationState();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
