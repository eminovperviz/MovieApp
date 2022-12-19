using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MovieApp.Persistence;

public static class ConfigureServices
{

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqlServer");
        services.AddDbContext<MovieAppContext>(options =>
            options.UseSqlServer(connectionString, sqlServerOptionsAction =>
            {
                //sqlServerOptionsAction.EnableRetryOnFailure(3);
                sqlServerOptionsAction.CommandTimeout(10);
            })
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution));

        services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
        services.AddScoped(typeof(IEFUnitOfWork), typeof(EFUnitOfWork));

        return services;
    }
}
