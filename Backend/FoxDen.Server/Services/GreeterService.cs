//
//  GreeterService.cs
//
//  Author:
//       Naka-Kon Contributors
//
//  Copyright (c) 2021 Naka-Kon. All rights reserved.
//

using System.Threading.Tasks;
using FoxDen.Server;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace FoxDen.Server.Services
{
    /// <summary>
    /// Example proto service implementation.
    /// </summary>
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GreeterService"/> class.
        /// </summary>
        /// <param name="logger">A logger for this type.</param>
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// An implementation of the Hello method.
        /// </summary>
        /// <param name="request">The user's request.</param>
        /// <param name="context">Additional information about the request.</param>
        /// <returns>A reply.</returns>
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
