using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitcoinApp.Infrastructure.Persistence;

public static class Extensions
{
    internal static DbContextOptionsBuilder ConfigureDatabase(this DbContextOptionsBuilder builder,
        string connectionString)
    {
        builder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));
        return builder.UseSqlServer(connectionString,
            b => b.MigrationsAssembly(typeof(Extensions).Assembly.GetName().Name));
    }

    public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.AddOptions<DatabaseOptions>().BindConfiguration(nameof(DatabaseOptions))
            .ValidateDataAnnotations();

        // Apply migrations at startup
        using (IServiceScope? scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            AppDbContext? dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        return builder;
    }

    public static IServiceCollection BindDbContext<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<TContext>((sp, options) =>
        {
            string? connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("master");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("The connection string 'master' is not configured.");
            }

            options.ConfigureDatabase(connectionString);
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

        return services;
    }
}
