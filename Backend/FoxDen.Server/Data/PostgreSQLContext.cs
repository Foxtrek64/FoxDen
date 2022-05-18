//
//  PostgreSQLContext.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

using FoxDen.Server.AppConfigs;
using Microsoft.EntityFrameworkCore;

namespace FoxDen.Server.Data
{
    /// <summary>
    /// A DbContext for use with PostgreSQL.
    /// </summary>
    public class PostgreSQLContext : FoxDenContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLContext"/> class.
        /// </summary>
        /// <param name="databaseOptions">The provided database options.</param>
        public PostgreSQLContext(DatabaseOptions databaseOptions)
            : base(databaseOptions)
        {
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_databaseOptions.DatabaseConnectionString);
        }
    }
}
