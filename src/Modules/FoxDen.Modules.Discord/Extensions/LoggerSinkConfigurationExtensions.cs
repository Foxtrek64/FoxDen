//
//  LoggerSinkConfigurationExtensions.cs
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
using System.Reflection.PortableExecutable;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Configuration;

namespace FoxDen.Modules.Discord.Extensions
{
    /// <summary>
    /// Provides extensions for <see cref="LoggerSinkConfiguration"/> instances.
    /// </summary>
    public static class LoggerSinkConfigurationExtensions
    {
        private const string OpenTelemetryEndpoint = "OTEL_EXPORTER_OTLP_ENDPOINT";
        private const string OpenTelemetryHeaders = "OTEL_EXPORTER_OTLP_HEADERS";
        private const string OpenTelemetryResourceAttributes = "OTEL_RESOURCE_ATTRIBUTES";
        private const string OpenTelemetryServiceName = "OTEL_SERVICE_NAME";
        private const string ServiceNameKey = "service.name";

        /// <summary>
        /// Adds the Aspire Event Source as an sink.
        /// </summary>
        /// <param name="sink">The logger configuration to modify.</param>
        /// <param name="configuration">The app configuration for retrieving OpenTelemetry parameters.</param>
        /// <returns>A <see cref="LoggerConfiguration"/> for chaining.</returns>
        public static LoggerConfiguration AspireEventSource(this LoggerSinkConfiguration sink, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(sink);
            ArgumentNullException.ThrowIfNull(configuration);

            return sink.OpenTelemetry(options =>
                    {
                        options.IncludedData |= Serilog.Sinks.OpenTelemetry.IncludedData.TraceIdField | Serilog.Sinks.OpenTelemetry.IncludedData.SpanIdField;
                        options.Endpoint = configuration[OpenTelemetryEndpoint];
                        AddHeaders(options.Headers, configuration[OpenTelemetryHeaders]);
                        AddResourceAttributes(options.ResourceAttributes, configuration[OpenTelemetryResourceAttributes]);
                        var serviceName = configuration[OpenTelemetryServiceName] ?? "Unknown";
                        options.ResourceAttributes.Add(ServiceNameKey, serviceName);
                    });

            void AddHeaders(IDictionary<string, string> headers, string? headerConfig)
            {
                if (!string.IsNullOrEmpty(headerConfig))
                {
                    var configHeaders = headerConfig.Split(',');
                    foreach ((string header, string? key, string? value) in AsKeyValuePairs(configHeaders))
                    {
                        if (key is not null && value is not null)
                        {
                            headers.Add(key, value);
                        }
                        else
                        {
                            Serilog.Debugging.SelfLog.WriteLine("Invalid header format: {0}", header);
                        }
                    }
                }
            }

            void AddResourceAttributes(IDictionary<string, object> attributes, string? attributeConfig)
            {
                if (!string.IsNullOrEmpty(attributeConfig))
                {
                    var configAttributes = attributeConfig.Split(",");
                    foreach ((string attribute, string? key, string? value) in AsKeyValuePairs(configAttributes))
                    {
                        if (key is not null && value is not null)
                        {
                            attributes.Add(key, value);
                        }
                        else
                        {
                            Serilog.Debugging.SelfLog.WriteLine("Invalid attribute format: {0}", attribute);
                        }
                    }
                }
            }
        }

        private static IEnumerable<(string RawValue, string? Key, string? Value)> AsKeyValuePairs(string[] headers)
        {
            foreach (var header in headers)
            {
                string[] parts = header.Split('=');
                yield return parts.Length switch
                {
                    2 => (header, parts[0], parts[1]),
                    _ => (header, null, null)
                };
            }
        }
    }
}
