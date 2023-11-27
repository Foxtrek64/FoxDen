//
//  OidcConfigurationController.cs
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

using JetBrains.Annotations;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoxDen.Web.Server.Controllers
{
    /// <summary>
    /// Provides a controller for providing an OIDC Configuration.
    /// </summary>
    [UsedImplicitly]
    public class OidcConfigurationController : Controller
    {
        private readonly ILogger<OidcConfigurationController> _logger;

        /// <summary>
        /// Gets the current instance an <see cref="IClientRequestParametersProvider" />.
        /// </summary>
        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OidcConfigurationController"/> class.
        /// </summary>
        /// <param name="clientRequestParametersProvider">Client request parameters.</param>
        /// <param name="logger">A logger for this instance.</param>
        public OidcConfigurationController
        (
            IClientRequestParametersProvider clientRequestParametersProvider,
            ILogger<OidcConfigurationController> logger
        )
        {
            ClientRequestParametersProvider = clientRequestParametersProvider;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the configuration for the specified client.
        /// </summary>
        /// <param name="clientId">The client id from the path.</param>
        /// <returns>The client parameters.</returns>
        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
            return Ok(parameters);
        }
    }
}
