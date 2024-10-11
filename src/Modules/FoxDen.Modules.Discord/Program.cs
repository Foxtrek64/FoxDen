//
//  Program.cs
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
using System.Threading.Tasks;
using FoxDen.Modules.Discord.Commands;
using FoxDen.Modules.Discord.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Remora.Commands.Extensions;
using Remora.Discord.API.Abstractions.Gateway.Commands;
using Remora.Discord.Caching.Abstractions.Services;
using Remora.Discord.Caching.Extensions;
using Remora.Discord.Caching.Redis.Services;
using Remora.Discord.Commands.Extensions;
using Remora.Discord.Gateway;
using Remora.Discord.Hosting.Extensions;
using Serilog;

namespace FoxDen.Modules.Discord
{
    /// <summary>
    /// The main entry point of the Discord module.
    /// </summary>
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // Discord
            builder.Services.AddDiscordService(_ => builder.Configuration.GetValue<string?>("Discord:BotToken") ?? throw new InvalidOperationException("No bot token has been provided."));

            // Services
            builder.AddRedisDistributedCache("CacheDen");
            builder.Services.AddDiscordCaching();
            builder.Services.TryAddSingleton<ICacheProvider, RedisCacheProvider>();

            builder.Services.Configure<DiscordGatewayClientOptions>(config => config.Intents |=
                GatewayIntents.AutoModerationConfiguration |
                GatewayIntents.AutoModerationExecution |
                GatewayIntents.Guilds |
                GatewayIntents.GuildBans |
                GatewayIntents.GuildEmojisAndStickers |
                GatewayIntents.GuildIntegrations |
                GatewayIntents.GuildInvites |
                GatewayIntents.GuildMembers |
                GatewayIntents.GuildMessagePolls |
                GatewayIntents.GuildMessageReactions |
                GatewayIntents.GuildMessages |
                GatewayIntents.GuildScheduledEvents |
                GatewayIntents.MessageContents
            );

            builder.Services.AddDiscordCommands(enableSlash: true)
                .AddCommandTree()
                .WithCommandGroup<HttpCatCommands>();

            Serilog.ILogger serilogLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.AspireEventSource(builder.Configuration)
                .CreateLogger();

            Log.Logger = serilogLogger;

            builder.Logging.ClearProviders().AddSerilog(serilogLogger, dispose: true);

            var host = builder.Build();

            try
            {
                await host.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex, "A fatal error has occurred: {message}", ex.Message);
                throw;
            }
        }
    }
}
