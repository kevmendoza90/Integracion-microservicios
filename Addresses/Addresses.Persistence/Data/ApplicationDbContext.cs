using Addresses.Domain.Base;
using Addresses.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Addresses.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Position> Locations { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=KEVIN;Database=MARKETING_ACTIVITIES;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

            }
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.Entity<Country>(entity =>
            {
                entity.ToTable("Addresses_Country");
            });
            model.Entity<City>(entity =>
            {
                entity.ToTable("Addresses_City");
            });

            model.Entity<Position>(entity =>
            {
                entity.ToTable("Addresses_Position");
            });
            model.Entity<State>(entity =>
            {
                entity.ToTable("Addresses_State");
            });
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
        }
        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedDate = utcNow;
                            entry.Property("CreatedDate").IsModified = false;
                            break;

                        case EntityState.Added:
                            trackable.InternalId = Guid.NewGuid();
                            trackable.CreatedDate = utcNow;
                            trackable.UpdatedDate = utcNow;
                            trackable.IsActive = true;
                            break;
                    }
                }
            }
        }
    }
}
