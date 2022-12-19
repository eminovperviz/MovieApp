using Microsoft.EntityFrameworkCore.Diagnostics;
using MovieApp.Application.Contracts.Interfaces;
using MovieApp.Persistence.Extensions;

namespace MovieApp.Persistence;

#pragma warning disable IDE1006 // Naming Styles
public class MovieAppContext : DbContext
#pragma warning restore IDE1006 // Naming Styles
{
    private readonly ICurrentUserService _currentUserService;

    public MovieAppContext(DbContextOptions<MovieAppContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
        ChangeDeleteBehaviorToNoAction(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            //other automated configurations left out
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSoftDeleteQueryFilter();
            }
        }

        base.OnModelCreating(modelBuilder);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.LogTo(Console.WriteLine, (id, level) => id == RelationalEventId.CommandExecuted);



    public new async Task<int> SaveChangesAsync(bool isCommit, CancellationToken cancellationToken = new CancellationToken())
    {
        int result = 0;

        if (isCommit)
        {
            TrackChanges();

            result = await base.SaveChangesAsync(cancellationToken);

            foreach (var entry in ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
        }

        return result;
    }

    private void TrackChanges()
    {
        // set Audit properties value
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                entry.Entity.CreatedBy = _currentUserService.UserId;
                entry.Entity.CreatedDate = DateTime.Now;
                break;
                case EntityState.Modified:
                entry.Entity.LastModifiedBy = _currentUserService.UserId;
                entry.Entity.LastModifiedDate = DateTime.Now;
                break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                entry.State = EntityState.Unchanged;
                entry.Entity.IsDeleted = true;
                break;
            }
        }
    }

    //SQL Server doesn't support ON DELETE RESTRICT, so ON DELETE NO ACTION is used instead.
    private static void ChangeDeleteBehaviorToNoAction(ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetForeignKeys())
                     .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.NoAction;
    }
}

