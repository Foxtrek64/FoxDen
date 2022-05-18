//
//  MicrosoftSQLContext.cs
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
    /// A DbContext for use with Microsoft SQL Server.
    /// </summary>
    public sealed class MicrosoftSQLContext : FoxDenContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MicrosoftSQLContext"/> class.
        /// </summary>
        /// <param name="databaseOptions">The provided database options.</param>
        public MicrosoftSQLContext(DatabaseOptions databaseOptions)
            : base(databaseOptions)
        {
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_databaseOptions.DatabaseConnectionString);
        }
    }
}
