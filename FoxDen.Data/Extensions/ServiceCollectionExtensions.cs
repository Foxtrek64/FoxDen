using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace FoxDen.Data.Extensions
{
    /// <summary>
    /// Provides a set of extensions for <see cref="IServiceCollection"/>
    /// </summary>
    [PublicAPI]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the <paramref name="serviceCollection"/> with the necessary services for FoxDen.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to configure.</param>
        /// <returns>The current <see cref="IServiceCollection"/> for chaining.</returns>
        public static IServiceCollection AddFoxDen(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }
    }
}
