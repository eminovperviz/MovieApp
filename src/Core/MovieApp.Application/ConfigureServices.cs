using Scrutor;
using System.Reflection;

namespace MovieApp.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.Scan(scan => scan.FromAssemblies(
                typeof(IApplicationAssemblyMarker).Assembly
            )
        .AddClasses(@class =>
              @class.Where(type =>
                    !type.Name.StartsWith('I')
                    && type.Name.EndsWith("Service")
                     )
           )
       .UsingRegistrationStrategy(RegistrationStrategy.Skip)
       .AsImplementedInterfaces()
       .WithScopedLifetime());

        services.AddApplicationContracts();

        return services;
    }
}
