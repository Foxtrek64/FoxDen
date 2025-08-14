using FoxDen.Commander.Data.Models;
using FoxDen.Core.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FoxDen.Commander.Data
{
    public sealed class CommanderDbContext(DbContextOptions<CommanderDbContext> context) : FoxDenDbContext("dbo", context)
    {
        public DbSet<NavMenuItem> NavigationMenuItems => Set<NavMenuItem>();
    }
}
