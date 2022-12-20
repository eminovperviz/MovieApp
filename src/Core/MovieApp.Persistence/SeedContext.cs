using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MovieApp.Persistence;
public static class SeedContext
{
    public static void MigrateDatabase(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetService<MovieAppContext>();
        dbContext.Database.Migrate();
    }
}
