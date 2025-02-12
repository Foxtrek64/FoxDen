//
//  IModule.cs
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

using System;
using System.Collections.Generic;
using System.Reflection;

namespace FoxDen.Data.Abstractions.Models
{
    /// <summary>
    /// Defines the shape of a module.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Gets the unique id of this module.
        /// </summary>
        Guid Id { get; init; }

        /// <summary>
        /// Gets or sets the name of this module.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the version of this module.
        /// </summary>
        Version Version { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the module is enabled.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets a list of components.
        /// </summary>
        IReadOnlyCollection<(string, string)> Components { get; }

        /// <summary>
        /// Gets a list of assets.
        /// </summary>
        IReadOnlyCollection<(string, string)> Assets { get; }

        /// <summary>
        /// Gets the assembly belonging to this module.
        /// </summary>
        Assembly? Assembly { get; init; }

        /// <summary>
        /// Gets the symbols belonging to this module.
        /// </summary>
        Assembly? Symbols { get; init; }
    }
}
