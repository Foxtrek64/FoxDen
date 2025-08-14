using k8s.Models;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var database = builder.AddPostgres("postgres")
    .WithDataVolume();
var catalog = database.AddDatabase("DataDen");

database.WithPgAdmin();

var apiService = builder.AddProject<Projects.FoxDen_ApiService>("apiservice");

builder.AddProject<Projects.FoxDen_Commander>("commander")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithReference(catalog)
    .WaitFor(catalog);

builder.Build().Run();
