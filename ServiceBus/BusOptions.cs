namespace ServiceBus;

public record BusOptions
{
    public const string AppSettingsKey = "BusOptions";
    public required string UserName { get; set; } = default!;
    public required string Password { get; set; } = default!;

    public required string Host { get; set; } = default!;
}