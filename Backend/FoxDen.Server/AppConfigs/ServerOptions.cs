//
//  ServerOptions.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

namespace FoxDen.Server.AppConfigs
{
    /// <summary>
    /// Represents a set of options for the server itself. All options are required for startup.
    /// </summary>
    public sealed class ServerOptions
    {
        /// <summary>
        /// The name for this section.
        /// </summary>
        public const string SectionName = "AppConfig:ServerOptions";

        /// <summary>
        /// Gets the address of the graphical front end of this web service.
        /// </summary>
        public string FrontEndAddress { get; init; } = "https://localhost:443";
    }
}
