using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalrTracking_Test.Models;
using SignalrTracking_Test.ViewModels;

namespace SignalrTracking_Test.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
     

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<UserVehicle> userVehicles { get; set; }
        public DbSet<MessageTrack> messageTracks { get; set; }
        public DbSet<DailyInformation> dailyInformation { get; set; }



    }
}