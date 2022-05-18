//
//  FoxDenContext.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

using System.Reflection;
using FoxDen.Server.AppConfigs;
using Microsoft.EntityFrameworkCore;

namespace FoxDen.Server.Data
{
    /// <summary>
    /// The default database provider for the core application.
    /// </summary>
    public abstract class FoxDenContext : DbContext
    {
        /// <summary>
        /// Gets the database options retrieved from the app config files.
        /// </summary>
#pragma warning disable SA1401 // Field used by implementing classes
        protected readonly DatabaseOptions _databaseOptions;
#pragma warning restore SA1401 // Fields should be private

        /// <summary>
        /// Initializes a new instance of the <see cref="FoxDenContext"/> class.
        /// </summary>
        /// <param name="databaseOptions">The provided database options.</param>
        public FoxDenContext(DatabaseOptions databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
