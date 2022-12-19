namespace MovieApp.Api.DependencyInjection.Api;

public static class ApiProjectInjection
{
    public static IServiceCollection AddApiProjectInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(o => o.AddPolicy("AllCors", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));

        services
            .AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalApiExceptionFilter));
                options.Filters.Add(typeof(GlobalValidationFilter));
            })
            .AddFluentValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
                options.RegisterValidatorsFromAssemblyContaining<IApplicationAssemblyMarker>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
             });

        services.AddSwaggerDocumentation();

        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });

        services.AddHttpContextAccessor();

        services.AddHttpClient();

        services.AddOptions();

        services.AddInfrastructure(configuration);

        services.AddApplication();

        return services;
    }
}
