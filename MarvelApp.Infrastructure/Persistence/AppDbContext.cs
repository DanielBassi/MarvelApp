using MarvelApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarvelApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Favorite> favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.OwnsOne(u => u.Email, email =>
                {
                    email.Property(e => e.Value)
                         .HasColumnName("Email")
                         .IsRequired();
                });

                entity.OwnsOne(u => u.Identification, identification =>
                {
                    identification.Property(e => e.Value)
                         .HasColumnName("Identification")
                         .IsRequired();
                });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
