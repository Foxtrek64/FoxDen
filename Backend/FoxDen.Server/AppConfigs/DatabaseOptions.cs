//
//  DatabaseOptions.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

namespace FoxDen.Server.AppConfigs
{
    /// <summary>
    /// Represents a set of options for the database connection. All options are required for startup.
    /// </summary>
    public sealed class DatabaseOptions
    {
        /// <summary>
        /// Gets the known name for this section.
        /// </summary>
        public const string SectionName = "AppConfig:DatabaseOptions";

        /// <summary>
        /// Gets a value indicating the kind of database to connect to.
        /// </summary>
        public DatabaseKind DatabaseKind { get; init; }

        /// <summary>
        /// Gets the connection string for connecting with the database.
        /// </summary>
        public string DatabaseConnectionString { get; init; }
    }
}
