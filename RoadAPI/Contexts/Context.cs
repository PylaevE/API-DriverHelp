using Microsoft.EntityFrameworkCore;
using RoadAPI.Entities;
using RoadAPI.Entities.User;
using RoadAPI.Entities.Vehicle;

namespace RoadAPI.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(p => p.Events)
                .WithOne()
                .HasForeignKey(p => p.AuthorId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        
        public DbSet<Vehicle> Vehicles { get; set; }
        
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
    }
}