using System.Diagnostics;
using MassTransit.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Observability;

public static class OpenTelemetryExt
{
    public static void AddOpenTelemetryExt(this IServiceCollection services, IConfiguration configuration)

    {
        var openTelemetryConstants =
            configuration.GetSection(OpenTelemetryOptions.AppSettingsKey).Get<OpenTelemetryOptions>()!;

        ActivitySourceProvider.Source =
            new ActivitySource(openTelemetryConstants.ActivitySourceName);


        services.AddOpenTelemetry()
            .WithTracing(traceConfig =>
            {
                traceConfig.AddSource(openTelemetryConstants.ActivitySourceName)
                    .AddSource(DiagnosticHeaders.DefaultListenerName)
                    .ConfigureResource(resource =>
                    {
                        resource.AddService(openTelemetryConstants.ServiceName,
                            serviceVersion: openTelemetryConstants.ServiceVersion);
                    });
                traceConfig.AddAspNetCoreInstrumentation(aspnetcoreOptions =>
                {
                    aspnetcoreOptions.Filter = context =>
                        !string.IsNullOrEmpty(context.Request.Path.Value) &&
                        context.Request.Path.Value.Contains("api", StringComparison.InvariantCulture);
                });

                traceConfig.AddEntityFrameworkCoreInstrumentation(efCoreOptions =>
                {
                    efCoreOptions.SetDbStatementForText = true;
                    efCoreOptions.SetDbStatementForStoredProcedure = true;
                });

                traceConfig.AddHttpClientInstrumentation();

                traceConfig.AddRedisInstrumentation(redisOptions =>
                {
                    redisOptions.SetVerboseDatabaseStatements = true;
                });


                traceConfig.AddOtlpExporter();
            })
            .WithLogging(loggingConfig =>
            {
                loggingConfig.ConfigureResource(resource =>
                {
                    resource.AddService(openTelemetryConstants.ServiceName,
                        serviceVersion: openTelemetryConstants.ServiceVersion);
                });

                loggingConfig.AddOtlpExporter();
            })
            .WithMetrics(metricConfig =>
            {
                metricConfig.ConfigureResource(resource =>
                {
                    resource.AddService(openTelemetryConstants.ServiceName,
                        serviceVersion: openTelemetryConstants.ServiceVersion);
                });

                metricConfig.AddRuntimeInstrumentation();
                metricConfig.AddProcessInstrumentation();
                metricConfig.AddOtlpExporter();
            });
    }
}