

using Audit.EntityFramework;
using Business.Domain.Base;
using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Persistence.Data
{
    
    public class ApplicationDbContext : AuditDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        

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
            model.Entity<Company>(entity =>
            {
                entity.ToTable("Business_Company");
            });
            model.Entity<Customer>(entity =>
            {
                entity.ToTable("Business_Customer");
            });

            model.Entity<CustomerType>(entity =>
            {
                entity.ToTable("Business_CustomerType");
            });

            model.Entity<CustomerContact>(entity =>
            {
                entity.ToTable("Business_CustomerContact");
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
