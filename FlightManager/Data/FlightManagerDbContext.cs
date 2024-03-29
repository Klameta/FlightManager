using Data.Extentions;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class FlightManagerDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Flight>()
                .HasMany(f => f.Reservations)
                .WithOne(r => r.Flight)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Seed();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FlightManager; Integrated Security = true");
            }
        }

        public FlightManagerDbContext()
        {

        }
        public FlightManagerDbContext(DbContextOptions<FlightManagerDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

    }
}
