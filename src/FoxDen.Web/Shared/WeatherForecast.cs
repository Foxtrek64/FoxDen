//
//  WeatherForecast.cs
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

using System;

namespace FoxDen.Web.Shared
{
    /// <summary>
    /// A weather forecast model.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Gets or sets the date of the forecast.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature in Celcius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        /// Gets the temperature in Fahrenheit.
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
