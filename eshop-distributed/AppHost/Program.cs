var builder = DistributedApplication.CreateBuilder(args);

////////////////////////////////////////////////////////////////////////////////
// BACKING SERVICES
////////////////////////////////////////////////////////////////////////////////
var postgres = builder
    .AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postgres.AddDatabase("catalogdb");

var cache = builder
    .AddRedis("cache") // Assuming AddRedis is part of Aspire.Hosting
    .WithRedisInsight()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);


////////////////////////////////////////////////////////////////////////////////
// PROJECTS
////////////////////////////////////////////////////////////////////////////////
var catalog = builder
    .AddProject<Projects.Catalog>("catalog")
    .WithReference(catalogDb)
    .WaitFor(catalogDb);

builder
    .AddProject<Projects.Basket>("basket")
    .WithReference(cache)
    .WithReference(catalog)
    .WaitFor(cache);

builder.Build().Run();
