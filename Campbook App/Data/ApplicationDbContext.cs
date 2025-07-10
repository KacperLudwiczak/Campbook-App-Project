using Campbook_App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Campbook_App.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Campground> Campgrounds { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Campground)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CampgroundId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Campground>()
            .OwnsOne(c => c.Geometry);
    }
}
