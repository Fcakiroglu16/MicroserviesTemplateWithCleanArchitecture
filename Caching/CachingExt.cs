using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Caching;

public static class CachingExt
{
    public static void AddCachingExt(this IServiceCollection services, IConfiguration configuration)
    {
        var cachingOptions = configuration.GetSection(CachingOptions.AppSettingsKey).Get<CachingOptions>()!;


        var optionsConfiguration =
            $"{cachingOptions.Host}:{cachingOptions.Port},password={cachingOptions.Password}";


        services.AddStackExchangeRedisCache(options => { options.Configuration = optionsConfiguration; });
    }
}