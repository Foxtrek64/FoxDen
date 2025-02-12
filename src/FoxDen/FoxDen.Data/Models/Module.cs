//
//  Module.cs
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
using FoxDen.Data.Abstractions.Models;

namespace FoxDen.Data.Models
{
    /// <summary>
    /// Represents a plugin module.
    /// </summary>
    /// <param name="id">The well-known id of the module.</param>
    /// <param name="name">The name of module.</param>
    /// <param name="version">The version of the module.</param>
    /// <param name="isEnabled">A value indicating whether the module is enabled.</param>
    /// <param name="components">A set of components.</param>
    /// <param name="assets">A set of assets.</param>
    /// <param name="assembly">The assembly owning this module.</param>
    /// <param name="symbols">The symbols owning this module.</param>
    public sealed class Module
    (
        Guid id,
        string name,
        Version version,
        bool isEnabled,
        List<(string, string)> components,
        List<(string, string)> assets,
        Assembly? assembly,
        Assembly? symbols
    ) : IModule
    {
        /// <inheritdoc />
        public Guid Id { get; init; } = id;

        /// <inheritdoc />
        public string Name { get; set; } = name;

        /// <inheritdoc />
        public Version Version { get; set; } = version;

        /// <inheritdoc />
        public bool IsEnabled { get; set; } = isEnabled;

        /// <inheritdoc />
        public IReadOnlyCollection<(string, string)> Components { get; } = components;

        /// <inheritdoc />
        public IReadOnlyCollection<(string, string)> Assets { get; } = assets;

        /// <inheritdoc />
        public Assembly? Assembly { get; init; } = assembly;

        /// <inheritdoc />
        public Assembly? Symbols { get; init; } = symbols;
    }
}
