IDistributedApplicationBuilder? builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<SqlServerServerResource>? sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent).WithDataVolume();

IResourceBuilder<SqlServerDatabaseResource>? db = sql.AddDatabase("master");

IResourceBuilder<RabbitMQServerResource>? rabbitmq = builder.AddRabbitMQ("messaging").WithManagementPlugin();

IResourceBuilder<ProjectResource>? apiService = builder.AddProject<Projects.BitcoinApp_WebApi>("webApi")
    .WithReference(db)
    .WaitFor(db).WithReference(rabbitmq).WaitFor(rabbitmq);

builder.AddProject<Projects.BitcoinApp_Web>("webFrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

DistributedApplication? app = builder.Build();

await app.RunAsync();
