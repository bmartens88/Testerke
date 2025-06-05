var builder = DistributedApplication.CreateBuilder(args);

var googleClientId = builder.AddParameter("googleClientId", secret: true);
var googleClientSecret = builder.AddParameter("googleClientSecret", secret: true);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

if (builder.ExecutionContext.IsRunMode)
    postgres.WithDataVolume();

var userDb = postgres.AddDatabase("userDb");

var api = builder.AddProject<Projects.Testerke_Api>("api")
    .WithReference(userDb)
    .WaitFor(userDb);

var idp = builder.AddProject<Projects.Testerke_Idp>("idp")
    .WithEnvironment("Authentication__Schemes__Google__ClientId", googleClientId)
    .WithEnvironment("Authentication__Schemes__Google__ClientSecret", googleClientSecret);

builder.AddProject<Projects.MigrationService>("migrations")
    .WithReference(userDb)
    .WaitFor(userDb)
    .WithHttpHealthCheck("/health")
    .WithHttpCommand("/reset-db", "Reset Database", commandOptions: new() { IconName = "DatabaseLightning" })
    .WithParentRelationship(postgres);

builder.Build().Run();