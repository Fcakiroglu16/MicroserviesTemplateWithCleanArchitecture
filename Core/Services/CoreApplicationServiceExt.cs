using System.Reflection;
using Asp.Versioning;
using Caching;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Observability;
using ServiceBus;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Services;

public static class CoreApplicationServiceExt
{
    public static IServiceCollection AddCoreApplicationService(this IServiceCollection services, Assembly assembly,
        IConfiguration configuration)
    {
        services.AddBusExt(configuration);
        services.AddCachingExt(configuration);
        services.AddOpenTelemetryExt(configuration);
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(assembly);

        AddSwaggerForVersioning(services);

        return services;
    }

    private static void AddSwaggerForVersioning(IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;


            options.ApiVersionReader = ApiVersionReader.Combine(
                new HeaderApiVersionReader("x-version")
            );
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}