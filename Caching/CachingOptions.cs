namespace Caching;

public record CachingOptions
{
    public const string AppSettingsKey = "CachingOptions";
    public required string Host { get; set; } = default!;

    public required string Port { get; set; } = default!;
    public required string Password { get; set; } = default!;
}