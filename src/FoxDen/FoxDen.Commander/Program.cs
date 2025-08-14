using Microsoft.FluentUI.AspNetCore.Components;
using FoxDen.Commander.Components;
using FoxDen.Commander.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FoxDen.Core.Data.Extensions;
using System;
using Microsoft.Extensions.Options;
using Remora.Plugins.Services;
using Microsoft.Extensions.Logging.Configuration;

namespace FoxDen.Commander;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddFluentUIComponents();

        builder.AddConfiguredSchemaAwareDbContextPool<CommanderDbContext>();

        // Plugins
        var pluginOptions = Options.Create(new PluginServiceOptions(Array.Empty<string>()));
        var pluginService = new PluginService(pluginOptions);

        var plugins = pluginService.LoadPluginTree();

        builder.Services.Configure<ServiceProviderOptions>(it =>
        {
            it.ValidateScopes = true;
            it.ValidateOnBuild = true;
        });

        builder.Services
            .AddSingleton(pluginService);

        var configurePlugins = plugins.ConfigureServices(builder.Services);
        if (!configurePlugins.IsSuccess)
        {
            throw new InvalidOperationException();
        }

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

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
