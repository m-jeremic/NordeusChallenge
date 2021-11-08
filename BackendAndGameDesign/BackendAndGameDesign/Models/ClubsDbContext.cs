using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAndGameDesign.Models
{
    public class ClubsDbContext : DbContext
    {
        public ClubsDbContext(DbContextOptions<ClubsDbContext> context) : base(context)
        {

        }

        public DbSet<Club> Clubs { get; set; }

        public DbSet<Player> Players { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>()
                .HasMany(e => e.Players);
        }
    }
}
