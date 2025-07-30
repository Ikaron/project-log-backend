using Microsoft.EntityFrameworkCore;
using ProjectLog.Api.Models;

namespace ProjectLog.Api.Data;

public class ProjectLogDbContext(DbContextOptions<ProjectLogDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("Projects");
        modelBuilder.Entity<Project>()
            .HasPartitionKey(nameof(Project.Id))
            .HasNoDiscriminator()
            .ToContainer("Projects");
    }
}