//
//  AppBarRegistration.cs
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

using Microsoft.FluentUI.AspNetCore.Components;

namespace FoxDen.Data.Models
{
    /// <summary>
    /// Describes an AppBarRegistration entity.
    /// </summary>
    public sealed class AppBarRegistration
    {
        /// <summary>
        /// Gets the unique id of this app bar registration.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets or sets the owner application.
        /// </summary>
        public required AppRegistration OwnerApplication { get; set; }

        /// <summary>
        /// Gets or sets the url for this item.
        /// </summary>
        public string? Href { get; set; }

        /// <summary>
        /// Gets or sets the icon to use when the item is not hovered/selected/active.
        /// </summary>
        public required Icon IconRest { get; set; }

        /// <summary>
        /// Gets or sets the icon to use when the item is hovered/selected/active.
        /// </summary>
        public Icon? IconActive { get; set; }

        /// <summary>
        /// Gets or sets the text to show under the icon.
        /// </summary>
        public required string Text { get; set; }

        /// <summary>
        /// Gets or sets the tooltip to show when the item is hovered.
        /// </summary>
        public string? Tooltip { get; set; }

        /// <summary>
        /// Gets or sets the count to show on the item with a <see cref="FluentCounterBadge"/>.
        /// </summary>
        public int? Count { get; set; }
    }
}
