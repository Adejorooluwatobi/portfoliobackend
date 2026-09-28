using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Infrastructure.Data;
using Portfolio.Infrastructure.Data.Seed;
using Portfolio.Infrastructure.Services;

namespace Portfolio.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rawConnection = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
            ?? Environment.GetEnvironmentVariable("DATABASE_URL")
            ?? (Environment.GetEnvironmentVariable("DB_HOST") != null
                ? $"Host={Environment.GetEnvironmentVariable("DB_HOST")};Port={Environment.GetEnvironmentVariable("DB_PORT") ?? "5432"};Database={Environment.GetEnvironmentVariable("DB_NAME")};Username={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASS")};"
                : configuration.GetConnectionString("DefaultConnection") 
                    ?? "Host=localhost;Port=5432;Database=portfolio_db;Username=postgres;Password=1234;");

        var connectionString = FormatNpgsqlConnectionString(rawConnection);

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<DataSeeder>();

        return services;
    }

    private static string FormatNpgsqlConnectionString(string rawConnection)
    {
        if (string.IsNullOrWhiteSpace(rawConnection)) return rawConnection;

        // If it starts with postgres:// or postgresql://, parse the URI into Npgsql key-value pairs
        if (rawConnection.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) ||
            rawConnection.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                var uri = new Uri(rawConnection);
                var userInfo = uri.UserInfo.Split(':', 2);
                var user = userInfo.Length > 0 ? Uri.UnescapeDataString(userInfo[0]) : "";
                var pass = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : "";
                var host = uri.Host;
                var port = uri.Port > 0 ? uri.Port : 5432;
                var database = uri.AbsolutePath.TrimStart('/');

                return $"Host={host};Port={port};Database={database};Username={user};Password={pass};SSL Mode=Require;Trust Server Certificate=true;";
            }
            catch
            {
                return rawConnection;
            }
        }

        // If it's already key-value format, ensure SSL Mode is configured for remote cloud hosts
        if (!rawConnection.Contains("localhost", StringComparison.OrdinalIgnoreCase) &&
            !rawConnection.Contains("127.0.0.1") &&
            !rawConnection.Contains("SSL Mode", StringComparison.OrdinalIgnoreCase) &&
            !rawConnection.Contains("SslMode", StringComparison.OrdinalIgnoreCase))
        {
            return rawConnection.TrimEnd(';') + ";SSL Mode=Require;Trust Server Certificate=true;";
        }

        return rawConnection;
    }
}
