using System.Reflection;
using Testerke.Api.Middleware;
using Testerke.Common.Application;
using Testerke.Common.Infrastructure;
using Testerke.Common.Presentation.Endpoints;
using Testerke.Modules.Users.Application;
using Testerke.Modules.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

Assembly[] moduleApplicationAssemblies =
[
    AssemblyReference.Assembly
];

builder.Services.AddApplication(moduleApplicationAssemblies);
builder.Services.AddInfrastructure();

builder.Services.AddUsersModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Something swagger like
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapEndpoints();
app.MapDefaultEndpoints();

app.Run();