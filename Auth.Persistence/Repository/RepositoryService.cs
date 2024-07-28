using Auth.Domain;
using Auth.Domain.Const;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.Repository;

public static class RepositoryService
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString(RepositoryConst.PostgresConnectionStrings);
            var migrationsAssemblyName = typeof(InfrastructureAssembly).Assembly.FullName;
            options.UseNpgsql(connectionString, x => x.MigrationsAssembly(migrationsAssemblyName));
        });

        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();

        return services;
    }
}