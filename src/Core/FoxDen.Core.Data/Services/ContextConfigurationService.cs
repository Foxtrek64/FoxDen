//
//  ContextConfigurationService.cs
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
using System.Collections.Generic;
using FoxDen.Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoxDen.Core.Data.Services
{
    /// <summary>
    /// Provides functionality for schema-aware database contexts.
    /// </summary>
    public static class ContextConfigurationService
    {
        // Service for accessing locally bundled assets.
        //// private readonly ContentService _content;

        private static readonly Dictionary<Type, string> _knownSchemas = [];

        private static bool EnsureSchemaIsCached<TContext>(out string? schema)
            where TContext : FoxDenDbContext
        {
            if (_knownSchemas.TryGetValue(typeof(TContext), out schema))
            {
                return true;
            }

            var dummyOptions = new DbContextOptionsBuilder<TContext>().Options;
            if (Activator.CreateInstance(typeof(TContext), dummyOptions) is not TContext dummyContext)
            {
                return false;
            }

            schema = dummyContext.Schema;
            _knownSchemas.Add(typeof(TContext), schema);
            return true;
        }

        /// <summary>
        /// Configures the options of a schema-aware database context.
        /// </summary>
        /// <typeparam name="TContext">The context type.</typeparam>
        /// <param name="optionsBuilder">The options builder to configure.</param>
        public static void ConfigureSchemaAwareContext<TContext>(DbContextOptionsBuilder optionsBuilder)
            where TContext : FoxDenDbContext
        {
            if (!EnsureSchemaIsCached<TContext>(out string? schema))
            {
                throw new InvalidOperationException("Failed to configure schema");
            }

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseNpgsql(options => options.MigrationsHistoryTable(HistoryRepository.DefaultTableName + schema))
                .UseSnakeCaseNamingConvention();
        }
    }
}
