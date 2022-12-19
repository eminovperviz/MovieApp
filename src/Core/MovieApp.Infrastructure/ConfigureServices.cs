using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieApp.Infrastructure;
public static class ConfigureServices
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddPersistence(configuration);

        JwtTokenConfiguration(services, configuration);

        services.Scan(sc =>
          sc.FromAssemblies(typeof(IInfrastructureAssemblyMarker).Assembly)
          .AddClasses(@class => @class.Where(type => !type.Name.StartsWith('I') && type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }

    private static void JwtTokenConfiguration(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtTokenConfig>(configuration.GetSection("JwtTokenConfig"));

        var jwtTokenConfig = new JwtTokenConfig();

        configuration.GetSection("JwtTokenConfig").Bind(jwtTokenConfig);

        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.IssuerSigningKey));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
              .AddJwtBearer(options =>
              {
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = jwtTokenConfig.ValidateIssuerSigningKey,
                      IssuerSigningKey = signingKey,
                      ValidateIssuer = jwtTokenConfig.ValidateIssuer,
                      ValidIssuer = jwtTokenConfig.ValidIssuer,
                      ValidateAudience = jwtTokenConfig.ValidateAudience,
                      ValidAudience = jwtTokenConfig.ValidAudience,
                      ValidateLifetime = jwtTokenConfig.ValidateLifetime,
                      ClockSkew = TimeSpan.Zero,
                      RequireExpirationTime = false,
                  };
              });

        services.AddAuthorization();

    }
}
