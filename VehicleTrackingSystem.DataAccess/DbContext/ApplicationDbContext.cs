using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using VehicleTrackingSystem.DataAccess.DBModels;

namespace VehicleTrackingSystem.DataAccess.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vehicle>().HasIndex(p => p.RegistrationNumber).IsUnique(true);
            base.OnModelCreating(builder);
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
