//
//  MauiProgram.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

using System.Runtime.ExceptionServices;
using Microsoft.Extensions.Logging;

namespace FoxDen.Maui
{
    /// <summary>
    /// This application's Program class.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// The equivalent of Main().
        /// </summary>
        /// <returns>A new <see cref="MauiApp"/>.</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // builder.Services.AddSingleton<WeatherForecastService>();

            try
            {
                // TODO: Run database migrations.
                var app = builder.Build();

                // Perform any post-build, pre-launch activities here.
                return app;
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
                throw;
            }
            finally
            {
                // TODO: Close and flush log
            }
        }
    }
}
