using Testerke.Api.Endpoints;
using Testerke.Api.Middleware;
using Testerke.Application;
using Testerke.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("userDb")
        ?? throw new ArgumentException("No value provided for connectionstring 'userDb'"),
    builder);

builder.Services.AddEndpoints();

var app = builder.Build();

app.UseExceptionHandler();

// app.UseAuthentication();

// app.UseAuthorization();

app.MapEndpoints();

app.MapDefaultEndpoints();

app.Run();