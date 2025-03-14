using BitcoinApp.Infrastructure.DependencyInjection;
using BitcoinApp.Infrastructure.MassTransit;
using BitcoinApp.Infrastructure.Persistence;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using Scalar.AspNetCore;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);


//// Add services to the container.
builder.AddServiceDefaults();
// Init Database with Aspire DB connection
builder.Services.AddControllers();

builder.ConfigureDependencyInjection();
// builder.AddSqlServerDbContext<AppDbContext>(connectionName: "database");
builder.ConfigureDatabase();


builder.ConfigureMassTransit();


builder.Services.AddOpenApi();


// Add Aspire related items
//builder.ConfigureOpenTelemetry();
//builder.AddDefaultHealthChecks();

WebApplication? app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "docker")
{
    app.MapOpenApi();
    app.MapScalarApiReference(_ => _.Servers = []);
}

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();
