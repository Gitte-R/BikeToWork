using BikeToWork.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BikeToWork.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Participant> Participants { get; set; }

        public DbSet<BikeRide> BikeRides { get; set; }
    }
}
