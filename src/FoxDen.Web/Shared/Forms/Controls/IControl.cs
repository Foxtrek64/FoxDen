//
//  IControl.cs
//
//  Author:
//       Naka-Kon Contributors <operations@naka-kon.com>
//
//  Copyright (c) Naka-Kon
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

namespace FoxDen.Web.Shared.Forms.Controls
{
    /// <summary>
    /// Represents the base of all input controls.
    /// </summary>
    /// <typeparam name="TValue">The type of the value stored in the control instance.</typeparam>
    internal interface IControl<TValue>
    {
        /// <summary>
        /// Gets or sets the return value of the item.
        /// </summary>
        TValue Value { get; set; }
    }
}
