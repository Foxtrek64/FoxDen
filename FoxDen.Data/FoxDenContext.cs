using System.Collections.Immutable;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace FoxDen.Data
{
    /// <summary>
    /// A database context for use by FoxDen.
    /// </summary>
    public sealed class FoxDenContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FoxDenContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public FoxDenContext(DbContextOptions<FoxDenContext> options)
            : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
