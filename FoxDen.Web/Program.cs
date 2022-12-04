//
//  Program.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

using FoxDen.Web;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FoxDen.Web
{
    /// <summary>
    /// The main entry point for this application.
    /// </summary>
    [PublicAPI]
    public class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMsalAuthentication
                (options => { builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication); });

            try
            {
                await using var app = builder.Build();
                await using var serviceScope = app.Services.CreateAsyncScope();

                // Run Database Migrations
                // var db = serviceScope.ServiceProvider.GetRequiredService<>();
                await app.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                return ex.HResult;
            }
        }
    }
}
