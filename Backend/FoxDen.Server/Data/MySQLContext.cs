//
//  MySQLContext.cs
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
    /// A DbContext for use with MySQL.
    /// </summary>
    public sealed class MySQLContext : FoxDenContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySQLContext"/> class.
        /// </summary>
        /// <param name="databaseOptions">The provided database options.</param>
        public MySQLContext(DatabaseOptions databaseOptions)
            : base(databaseOptions)
        {
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL(_databaseOptions.DatabaseConnectionString);
        }
    }
}
