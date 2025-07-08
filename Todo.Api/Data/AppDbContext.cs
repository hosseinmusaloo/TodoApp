using Microsoft.EntityFrameworkCore;

namespace Todo.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Tasks.Task> Tasks => Set<Tasks.Task>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureTaskTable(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureTaskTable(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Tasks.Task>();

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.Description)
               .HasMaxLength(500);

        builder.Property(p => p.DueDate)
               .HasColumnType("timestamp without time zone");
    }
}