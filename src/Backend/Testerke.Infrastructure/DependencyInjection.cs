using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Testerke.Application.Abstractions;
using Testerke.Domain.Users.Abstractions;
using Testerke.Infrastructure.Data;
using Testerke.Infrastructure.Users;

namespace Testerke.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure<TBuilder>(
        this IServiceCollection services,
        string connectionString,
        TBuilder builder)
        where TBuilder : IHostApplicationBuilder

    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        services.TryAddSingleton<DomainEventsInterceptor>();

        services.AddDbContext<UsersDbContext>((sp, options) =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
                .AddInterceptors(sp.GetRequiredService<DomainEventsInterceptor>())
                .UseSnakeCaseNamingConvention());

        builder.EnrichNpgsqlDbContext<UsersDbContext>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}