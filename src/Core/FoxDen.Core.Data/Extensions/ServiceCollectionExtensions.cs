//
//  ServiceCollectionExtensions.cs
//
//  Author:
//       LuzFaltex Contributors <support@luzfaltex.com>
//
//  Copyright (c) LuzFaltex, LLC.
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using Aspire.Npgsql.EntityFrameworkCore.PostgreSQL;
using FoxDen.Core.Data.Context;
using FoxDen.Core.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoxDen.Core.Data.Extensions
{
/// <summary>
/// Contains extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the schema-aware db context to the service collection.
    /// </summary>
    /// <typeparam name="TContext">The type of context to add.</typeparam>
    /// <param name="builder">The host builder to extend.</param>
    /// <param name="configureSettings">Optional settings to configure Entity Framework for Npgsql.</param>
    public static void AddConfiguredSchemaAwareDbContextPool<TContext>
    (
        this IHostApplicationBuilder builder,
        Action<NpgsqlEntityFrameworkCorePostgreSQLSettings>? configureSettings = null
    )
        where TContext : FoxDenDbContext
    {
        builder.AddNpgsqlDbContext<TContext>
        (
            "postgres",
            configureSettings: configureSettings,
            configureDbContextOptions: ContextConfigurationService.ConfigureSchemaAwareContext<TContext>
        );
    }
}
}
