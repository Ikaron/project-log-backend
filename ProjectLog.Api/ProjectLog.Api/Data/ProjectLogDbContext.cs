using Microsoft.EntityFrameworkCore;
using ProjectLog.Api.Models;

namespace ProjectLog.Api.Data;

public class ProjectLogDbContext(DbContextOptions<ProjectLogDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();

#if false // TODO: Add flag to toggle NoSQL support (Cosmos)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("Projects");
        modelBuilder.Entity<Project>()
            .HasPartitionKey(nameof(Project.Id))
            .HasNoDiscriminator()
            .ToContainer("Projects");
    }
#endif
}