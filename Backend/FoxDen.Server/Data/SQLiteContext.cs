//
//  SQLiteContext.cs
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
    /// A DbContext for use with SQLite.
    /// </summary>
    public class SQLiteContext : FoxDenContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteContext"/> class.
        /// </summary>
        /// <param name="databaseOptions">The provided database options.</param>
        public SQLiteContext(DatabaseOptions databaseOptions)
            : base(databaseOptions)
        {
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_databaseOptions.DatabaseConnectionString);
        }
    }
}
