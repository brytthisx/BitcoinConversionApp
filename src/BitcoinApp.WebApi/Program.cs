using BitcoinApp.Infrastructure.DependencyInjection;
using BitcoinApp.Infrastructure.MassTransit;
using BitcoinApp.Infrastructure.Persistence;
using Scalar.AspNetCore;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);


builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.ConfigureDependencyInjection();
builder.ConfigureDatabase();


builder.ConfigureMassTransit();


builder.Services.AddOpenApi();


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
