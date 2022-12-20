using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace MovieApp.Application.Contracts;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationContracts(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.Scan(scan => scan.FromAssemblies(typeof(IApplicationContractAssemblyMarker).Assembly)
        .AddClasses(@class =>
            @class.Where(type => !type.Name.StartsWith('I') && type.Name.EndsWith("Service")))
       .UsingRegistrationStrategy(RegistrationStrategy.Skip)
       .AsImplementedInterfaces()
       .WithScopedLifetime());

        return services;
    }
}

