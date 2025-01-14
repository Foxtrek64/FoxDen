using System.Reflection;

namespace FoxDen.Data.Abstractions
{
    /// <summary>
    /// Represents a plugin module.
    /// </summary>
    /// <param name="id">The well-known id of the module.</param>
    /// <param name="name">The name of module.</param>
    /// <param name="version">The version of the module.</param>
    /// <param name="IsEnabled">A value indicating whether the module is enabled.</param>
    /// <param name="components">A set of components.</param>
    /// <param name="assets">A set of assets.</param>
    /// <param name="assembly">The assembly owning this module.</param>
    /// <param name="symbols">The symbols owning this module.</param>
    public sealed class Module
    (
        Guid id,
        string name,
        Version version,
        bool IsEnabled,
        List<(string, string)> components,
        List<(string, string)> assets,
        Assembly? assembly,
        Assembly? symbols
    );
}
