using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VehicleParser
{
    public partial class u1225541_mainContext : DbContext
    {
        public u1225541_mainContext()
        {
        }

        public u1225541_mainContext(DbContextOptions<u1225541_mainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleBrand> VehicleBrands { get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=37.140.192.131; Database=u1225541_main; User ID=u1225541_main; Password=6d9L7yv&");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("u1225541_main")
                .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => e.AuthorId, "IX_Events_AuthorId");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.AuthorId);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasIndex(e => e.OwnerId, "IX_Vehicles_OwnerId");

                entity.HasIndex(e => e.VehicleModelId, "IX_Vehicles_VehicleModelId");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.OwnerId);

                entity.HasOne(d => d.VehicleModel)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleModelId);
            });

            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity.HasIndex(e => e.BrandId, "IX_VehicleModels_BrandId");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.VehicleModels)
                    .HasForeignKey(d => d.BrandId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
