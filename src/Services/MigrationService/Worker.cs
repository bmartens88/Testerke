using System.Diagnostics;
using Testerke.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MigrationService;

internal class Worker(IServiceProvider serviceProvider, ILogger<Worker> logger) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";

    private readonly ActivitySource _activitySource = new(ActivitySourceName);
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

        using var activity = _activitySource.StartActivity("Initializing user database", ActivityKind.Client);
        await InitializeDatabaseAsync(dbContext, stoppingToken);
    }

    public async Task InitializeDatabaseAsync(UsersDbContext dbContext, CancellationToken stoppingToken)
    {
        var sw = Stopwatch.StartNew();
        
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(dbContext.Database.MigrateAsync, stoppingToken);
        
        logger.LogInformation("Database initialization completed after {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
    }
}