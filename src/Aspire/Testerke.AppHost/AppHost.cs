var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

if (builder.ExecutionContext.IsRunMode)
    postgres.WithDataVolume();

var userDb = postgres.AddDatabase("userDb");

var api = builder.AddProject<Projects.Testerke_Api>("api")
    .WithReference(userDb)
    .WaitFor(userDb);

builder.Build().Run();