//
//  ServiceCollectionExtensions.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

using System;
using FoxDen.Server.AppConfigs;
using FoxDen.Server.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoxDen.Server.Extensions
{
    /// <summary>
    /// A set of extensions for adding components to the service provider.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all core services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configurationManager">A <see cref="ConfigurationManager"/> for referencing the current configuration.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddFoxDen(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddOptions<ServerOptions>()
                .Configure<IConfiguration>((options, configuration) => configuration.Bind(ServerOptions.SectionName, options))
                .Validate(options => !string.IsNullOrWhiteSpace(options.FrontEndAddress), $"{nameof(ServerOptions.FrontEndAddress)} cannot be null or empty.");

            services.AddDatabase(configurationManager);

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddOptions<DatabaseOptions>()
                .Configure<IConfiguration>((options, configuration) => configuration.Bind(DatabaseOptions.SectionName, options))
                .Validate(options => !string.IsNullOrWhiteSpace(options.DatabaseConnectionString), $"{DatabaseOptions.SectionName}:{nameof(DatabaseOptions.DatabaseConnectionString)} cannot be null or empty.");

            var servicesClone = services

            var dbKindValue = $"{DatabaseOptions.SectionName}:{nameof(DatabaseOptions.DatabaseKind)}";

            var dbKind = configurationManager.GetValue<DatabaseKind>(dbKindValue);

            return dbKind switch
            {
                DatabaseKind.SQLite => services.AddDbContext<FoxDenContext, SQLiteContext>(),
                DatabaseKind.MySQL => services.AddDbContext<FoxDenContext, MySQLContext>(),
                DatabaseKind.PostgreSQL => services.AddDbContext<FoxDenContext, PostgreSQLContext>(),
                DatabaseKind.MicrosoftSQL => services.AddDbContext<FoxDenContext, MicrosoftSQLContext>(),
                _ => throw new NotImplementedException($"{dbKindValue}: Support for the '{configurationManager[dbKindValue]}' database has not been implemented.")
            };

            throw new InvalidOperationException();
        }
    }
}
