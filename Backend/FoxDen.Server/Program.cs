//
//  Program.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FoxDen.Server.AppConfigs;
using FoxDen.Server.Extensions;
using FoxDen.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Remora.Plugins.Services;
using Remora.Results;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace FoxDen.Server
{
    /// <summary>
    /// The application entry point.
    /// </summary>
    public class Program
    {
        private const string DevEnvVar = "DOTNET_ENVIRONMENT";

        private const string LogMessageTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";

        private static readonly string AppDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LuzFaltex", "FoxDen", "Server");

        private static readonly string LogDir = Path.Combine(AppDir, "Logs");

        /// <summary>
        /// The app entry point.
        /// </summary>
        /// <param name="args">Command line arguments passed to the application.</param>
        /// <returns>An integer representing the result of the async operation. Zero indicates success while non-zero results indicate failure.</returns>
        public static int Main(string[] args)
        {
            var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

            Console.WriteLine($"FoxDen.Server version {fvi.FileVersion}");
            Console.WriteLine(fvi.LegalCopyright);
            Console.WriteLine("For internal use only.");

            var builder = WebApplication.CreateBuilder(args);

            // App Configuration
            builder.Configuration.AddEnvironmentVariables("FoxDen_");
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
            builder.Configuration.AddJsonFile(Path.Combine(AppDir, "appsettings.json"), optional: true, reloadOnChange: true);
            builder.Configuration.AddJsonFile(Path.Combine(AppDir, $"appsettings.{builder.Environment.EnvironmentName}.json"), optional: true, reloadOnChange: true);

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }

            // Logging
            Serilog.Core.Logger seriLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: LogMessageTemplate, theme: AnsiConsoleTheme.Code)
                .WriteTo.File(Path.Combine(LogDir, "Execution_.Log"), outputTemplate: LogMessageTemplate, rollingInterval: RollingInterval.Day)
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Logging.AddSerilog(seriLogger);
            Log.Logger = seriLogger;

            // Service provider
            // TODO: Is this needed anymore?
            // builder.Host.UseDefaultServiceProvider(x => x.ValidateScopes = true);

            // Services
            var pluginServiceOptions = new PluginServiceOptions(new List<string> { "./Plugins" });
            var pluginService = new PluginService(Options.Create(pluginServiceOptions));

            var plugins = pluginService.LoadPluginTree();
            var configurePluginsResult = plugins.ConfigureServices(builder.Services);
            if (!configurePluginsResult.IsSuccess)
            {
                if (configurePluginsResult.Error is ExceptionError exe)
                {
                    Log.ForContext<Program>()
                        .Fatal(exe.Exception, "Failed to load plugins! {Error}", exe.Message);

                    return exe.Exception.HResult;
                }

                Log.ForContext<Program>()
                    .Fatal("Failed to load plugins! {Error}", configurePluginsResult.Error.Message);

                return 1;
            }
            builder.Services.AddSingleton(pluginService);

            builder.Services.AddFoxDen(builder.Configuration);

            builder.Services.AddGrpc();

            using var host = builder.Build();

            try
            {
                // Configure the HTTP request pipeline.
                host.MapGrpcService<GreeterService>();

                var serverOptionsOpts = host.Services.GetRequiredService<IOptions<ServerOptions>>();
                var serverOptions = serverOptionsOpts.Value;

                // host.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                host.MapGet("/", (context) =>
                {
                    // Browsers which interact with the gRPC endpoint are
                    // redirected to the graphical front-end.
                    context.Response.Redirect(serverOptions.FrontEndAddress);

                    return Task.CompletedTask;
                });

                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.ForContext<Program>()
                    .Fatal(ex, "Host terminated unexpectedly.");

                if (Debugger.IsAttached && Environment.UserInteractive)
                {
                    Console.WriteLine(Environment.NewLine + "Press any key to exit...");
                    Console.ReadKey(true);
                }

                return ex.HResult;
            }
            finally
            {
                Log.CloseAndFlush();
                Log.Logger = null;
            }
        }
    }
}
