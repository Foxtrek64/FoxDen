//
//  FoxDenDbContext.cs
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

using Microsoft.EntityFrameworkCore;

namespace FoxDen.Core.Data.Context
{
    /// <summary>
    /// The base context for all FoxDen contexts.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="FoxDenDbContext"/> class.
    /// </remarks>
    /// <param name="schema">The database schema managed by this context.</param>
    /// <param name="contextOptions">The context options.</param>
    public abstract class FoxDenDbContext(string schema, DbContextOptions contextOptions) : DbContext(contextOptions)
    {
        /// <summary>
        /// Gets the schema of the database.
        /// </summary>
        public string Schema { get; } = schema;

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(Schema);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.GetSchema() == Schema)
                {
                    continue;
                }

                entityType.SetIsTableExcludedFromMigrations(true);
            }

            modelBuilder.HasPostgresExtension("fuzzystrmatch");
        }
    }
}
