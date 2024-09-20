using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Remora.Results;

namespace FoxDen.Modules.API.Models;

/// <summary>
/// Provides a base class for modules.
/// </summary>
/// <typeparam name="TModule">The type of module this base is describing.</typeparam>
[PublicAPI]
public abstract class ModuleBase<TModule>(ILogger<TModule> logger)
    where TModule : ModuleBase<TModule>
{
    private readonly Task<Result> _successfulResultTask = Task.FromResult(Result.FromSuccess());

    /// <summary>
    /// Called on module install. This should perform all registrations, such as adding ACP or Front End components,
    /// applying migrations, and any other changes. This is called when the module is installed the first time.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for this operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or containing the startup error.</returns>
    public virtual Task<Result> InstallAsync(CancellationToken cancellationToken)
    {
        return _successfulResultTask;
    }

    /// <summary>
    /// Starts the module. This is called when the core application starts or the module is enabled
    /// and should register any transient handlers, such as Mediator events/commands or Hangfire events.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for this operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or containing the startup error.</returns>
    public virtual Task<Result> StartAsync(CancellationToken cancellationToken)
    {
        return _successfulResultTask;
    }

    /// <summary>
    /// Stops the module. This is called when the core application stops or the module is disabled
    /// and should unregister any transient handlers, such as Mediator events/commands or Hangfire events.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for this operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or containing the startup error.</returns>
    public virtual Task<Result> StopAsync(CancellationToken cancellationToken)
    {
        return _successfulResultTask;
    }

    /// <summary>
    /// Called on module uninstall. This should unregister any ACP or front-end components, drop database tables,
    /// and revert any changes made during plugin install. This is called after the user has accepted a warning of
    /// data destruction.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for this operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or containing the startup error.</returns>
    public virtual Task<Result> UninstallAsync(CancellationToken cancellationToken)
    {
        return _successfulResultTask;
    }
}