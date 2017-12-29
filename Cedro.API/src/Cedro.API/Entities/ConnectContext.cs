using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cedro.API.Entities
{

    public class ConnectContext : DbContext
    {
        public ConnectContext(DbContextOptions<ConnectContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Restaurantes> Restaurantes { get; set; }

        public DbSet<PratosRestaurantes> PratosRestaurantes { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
