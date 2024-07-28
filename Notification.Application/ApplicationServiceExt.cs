using Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Observability;
using ServiceBus;

namespace Auth.Application;

public static class ApplicationServiceExt
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBusExt(configuration);
        services.AddCachingExt(configuration);
        services.AddOpenTelemetryExt(configuration);
        return services;
    }
}