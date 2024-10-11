using FoxDen.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

// The default value of these parameters is set in the appsettings.json file.
// These values are modeled as parameters so they can be overridden for different environments, etc.
var keycloakRealmName = builder.AddParameter("keycloak-realm");
var keycloakRealmDisplayName = builder.AddParameter("keycloak-realm-display");
var dataDenClientId = builder.AddParameter("foxden-dataservice-client-id");
var dataDenClientName = builder.AddParameter("foxden-dataservice-client-name");
var commanderClientId = builder.AddParameter("foxden-commander-client-id");
var commanderClientName = builder.AddParameter("foxden-commander-client-name");
var commanderClientSecret = builder.AddParameter("foxden-commander-client-secret", secret: true)
    .WithGeneratedDefault(new GenerateParameterDefault() { MinLength = 32, Special = false });

var discordClientId = builder.AddParameter("discord-client-id");
var discordBotToken = builder.AddParameter("discord-bot-client-secret", secret: true);

// Data
var cache = builder.AddRedis("CacheDen");
var dataDen = builder.AddPostgres("database")
    .WithPgAdmin(containerBulder => containerBulder.WithImageTag("8.12"));

var dataDenDb = dataDen.AddDatabase("FoxDenData");
var dataLogDb = dataDen.AddDatabase("FoxDenLogs");

// Identity Provider
/*
var keycloak = builder.AddKeycloak("keycloak", 8080)
     .WithDataVolume();
*/

var keycloak = builder.AddKeycloak("keycloak")
    .WithImageTag("25.0")
    .WithDataVolume()
    .RunWithHttpsDevCertificate();

/*
var realm = keycloak.WithRealmImport("realms", isReadOnly: true)
    .WithEnvironment("REALM_NAME", keycloakRealmName)
    .WithEnvironment("REALM_DISPLAY_NAME", keycloakRealmDisplayName)
    .WithEnvironment("REALM_HSTS", builder.ExecutionContext.IsRunMode ? "" : "max-age=31536000; includeSubDomains");
*/

// APIs
var apiService = builder.AddProject<Projects.FoxDen_ApiService>("DataDen")
     .WithReference(keycloak)
     .WithEnvironment("Authentication__Keycloak__Realm", keycloakRealmName)
     .WithEnvironment("Authentication__Schemes__Bearer__ValidAudience", dataDenClientId);

Console.WriteLine(builder.Environment.EnvironmentName);

// Apps
builder.AddProject<Projects.FoxDen_Commander>("foxden-commander")
    .WithExternalHttpEndpoints()
        .WithReference(cache)
        .WithReference(dataDen)
        .WithReference(dataDen)
        .WithReference(apiService)
        .WithReference(keycloak)
        .WithEnvironment("Authentication__Keycloak__Realm", keycloakRealmName)
        .WithEnvironment("Authentication__Schemes__OpenIdConnect__ClientId", commanderClientId)
        .WithEnvironment("Authentication__Schemes__OpenIdConnect__ClientSecret", commanderClientSecret);

builder.AddProject<Projects.FoxDen_Modules_Discord>("foxden-modules-discord")
    .WithReference(cache)
    .WithReference(apiService)
    .WithReference(dataDen)
    .WithReference(dataLogDb)
    .WithEnvironment("Discord__ClientId", discordClientId)
    .WithEnvironment("Discord__BotToken", discordBotToken);

builder.Build().Run();
