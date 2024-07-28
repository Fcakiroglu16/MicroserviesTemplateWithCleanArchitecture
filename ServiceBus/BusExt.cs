using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBus;

public static class BusExt
{
    public static IServiceCollection AddBusExt(this IServiceCollection services, IConfiguration configuration)
    {
        // add masstransit

        services.AddMassTransit(configure =>
        {
            configure.UsingRabbitMq((context, rabbitConfigure) =>
            {
                var busOptions = configuration.GetSection(BusOptions.AppSettingsKey).Get<BusOptions>()!;

                rabbitConfigure.Host(busOptions.Host, hostConfigure =>
                {
                    hostConfigure.Username(busOptions.UserName);
                    hostConfigure.Password(busOptions.Password);
                });
            });
        });

        return services;
    }
}