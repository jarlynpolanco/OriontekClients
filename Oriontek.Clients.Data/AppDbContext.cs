using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oriontek.Clients.Data.Models;

namespace Oriontek.Clients.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>()
               .HasOne(a => a.Client)
               .WithMany(b => b.Addresses)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
