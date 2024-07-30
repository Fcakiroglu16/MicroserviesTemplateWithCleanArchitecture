namespace Observability;

public record OpenTelemetryOptions
{
    public const string AppSettingsKey = "OpenTelemetryOptions";
    public required string ServiceName { get; set; } = default!;

    public required string ServiceVersion { get; set; } = default!;

    public required string ActivitySourceName { get; set; } = default!;
}