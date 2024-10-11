using Microsoft.EntityFrameworkCore;

namespace FoxDen.Data
{
    /// <summary>
    /// Provides a data context for the application.
    /// </summary>
    /// <param name="options">Options for this application.</param>
    public sealed class FoxDenDbContext(DbContextOptions<FoxDenDbContext> options) : DbContext(options)
    {
    }
}
