//
//  FoxDenContext.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace FoxDen.Server.Data
{
    /// <summary>
    /// The default database provider for the core application.
    /// </summary>
    public abstract class FoxDenContext : DbContext
    {
        protected 
        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
