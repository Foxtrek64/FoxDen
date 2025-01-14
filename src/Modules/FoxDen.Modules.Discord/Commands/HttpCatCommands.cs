//
//  HttpCatCommands.cs
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
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using Remora.Commands.Attributes;
using Remora.Commands.Groups;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Objects;
using Remora.Discord.Commands.Attributes;
using Remora.Discord.Commands.Feedback.Services;
using Remora.Results;

namespace FoxDen.Modules.Discord.Commands;

/// <summary>
/// Defines the cat command which posts an image from http.cat.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="HttpCatCommands"/> class.
/// </remarks>
/// <param name="feedbackService">The feedback service.</param>
public class HttpCatCommands(FeedbackService feedbackService) : CommandGroup
{
    /// <summary>
    /// Posts a HTTP error code cat.
    /// This command will generate ephemeral responses.
    /// </summary>
    /// <param name="httpCode">The HTTP error code.</param>
    /// <returns>The result of the command.</returns>
    [Command("cat")]
    [Description("Posts a cat image that represents the given error code.")]
    public async Task<IResult> PostHttpCatAsync([Description("The HTTP code.")] int httpCode)
    {
        var embedImage = new EmbedImage($"https://http.cat/{httpCode}");
        var embed = new Embed(Colour: feedbackService.Theme.Secondary, Image: embedImage);

        return (Result)await feedbackService.SendContextualEmbedAsync(embed, ct: CancellationToken);
    }
}
