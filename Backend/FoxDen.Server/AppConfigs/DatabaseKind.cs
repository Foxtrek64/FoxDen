//
//  DatabaseKind.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

namespace FoxDen.Server.AppConfigs
{
    /// <summary>
    /// A set of supported database providers.
    /// </summary>
    public enum DatabaseKind
    {
        /// <summary>
        /// Designates a SQLite Database Provider.
        /// </summary>
        SQLite,

        /// <summary>
        /// Designates a MySQL Database Provider.
        /// </summary>
        MySQL,

        /// <summary>
        /// Designates a PostgreSQL Database Provider.
        /// </summary>
        PostgreSQL,

        /// <summary>
        /// Designates a Microsoft SQL Server Database Provider.
        /// </summary>
        MicrosoftSQL
    }
}
