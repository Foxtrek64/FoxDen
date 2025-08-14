//
//  DbContextExtensions.cs
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

using System.Linq;
using FoxDen.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FoxDen.Core.Data.Extensions
{
    /// <summary>
    /// Provides a set of extension methods for database contexts.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Normalizes an entity reference, replacing it with an already-tracked instance if one exists.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="context">The context to normalize from.</param>
        /// <param name="entity">The potentially foreign entity.</param>
        /// <returns>The entity, or another entity from the context with the same id.</returns>
        public static TEntity NormalizeReference<TEntity>(this DbContext context, TEntity entity)
            where TEntity : class, IEFEntity
        {
            var existingEntityEntry = context.ChangeTracker.Entries<TEntity>().FirstOrDefault(it => it.Entity.Id == entity.Id);

            if (existingEntityEntry is not null)
            {
                return existingEntityEntry.Entity;
            }

            context.Attach(entity);
            return entity;
        }
    }
}
