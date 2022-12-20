using Microsoft.EntityFrameworkCore.Diagnostics;
using MovieApp.Persistence.Extensions;

namespace MovieApp.Persistence;

#pragma warning disable IDE1006 // Naming Styles
public class MovieAppContext : DbContext
#pragma warning restore IDE1006 // Naming Styles
{

    public MovieAppContext(DbContextOptions<MovieAppContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.LogTo(Console.WriteLine, (id, level) => id == RelationalEventId.CommandExecuted);

    public new async Task<int> SaveChangesAsync(bool isCommit, CancellationToken cancellationToken = new CancellationToken())
    {
        int result = 0;

        if (isCommit)
        {
            result = await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}

