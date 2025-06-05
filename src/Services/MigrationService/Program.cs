using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MigrationService;
using Testerke.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<UsersDbContext>("userDb", null,
    optionsBuilder => optionsBuilder.UseNpgsql(npgsqlBuilder =>
        npgsqlBuilder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddSingleton<Worker>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<Worker>());
builder.Services.AddHealthChecks()
    .AddCheck<MigrationServiceHealthChecks>("MigrationService", null);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapPost("/reset-db",
        async (UsersDbContext dbContext, Worker worker, CancellationToken cancellationToken) =>
        {
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);
            await worker.InitializeDatabaseAsync(dbContext, cancellationToken);
        });
}

app.MapDefaultEndpoints();

await app.RunAsync();