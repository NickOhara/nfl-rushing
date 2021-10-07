using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nfl_rushing.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<PlayerStats> PlayerStats { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=NFLRushing.db");
        }
    }
}
