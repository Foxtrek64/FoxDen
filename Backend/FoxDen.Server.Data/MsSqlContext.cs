using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxDen.Server.Data
{
    public sealed class MsSqlContext : FoxDenContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }
    }
}
