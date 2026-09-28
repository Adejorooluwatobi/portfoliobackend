using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Application;
using Portfolio.Infrastructure;
using Portfolio.Infrastructure.Data;
using Portfolio.Infrastructure.Data.Seed;
using Portfolio.Api.Middleware;
using Serilog;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;

namespace Portfolio.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        // 1. Load .env file
        Env.TraversePath().Load();

        // 2. Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/portfolio-api-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            Log.Information("Starting up Portfolio API...");

            var builder = WebApplication.CreateBuilder(args);

            // Use Serilog for ASP.NET Core logging
            builder.Host.UseSerilog();

            // 3. Add Clean Architecture Layers
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // 4. Add Controllers
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            // 5. Configure JWT Authentication (Fallback to config if env is missing)
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_KEY") ?? builder.Configuration["Jwt:Secret"] ?? "OluwatobiPortfolioSuperSecretKey2025MustBeAtLeast32BytesLong!";
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? builder.Configuration["Jwt:Issuer"] ?? "PortfolioApi";
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? builder.Configuration["Jwt:Audience"] ?? "PortfolioAdmin";

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();

            // 6. Forwarded Headers for Cloud Reverse Proxies (Render, Cloudflare)
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownIPNetworks.Clear();
                options.KnownProxies.Clear();
            });

            // 7. Configure CORS (matching POS_SAAS: AllowAnyOrigin for unrestricted local & cloud access)
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
                options.AddPolicy("AllowPortfolioClients", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // 8. Configure OpenAPI
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // 9. HTTP Pipeline
            app.UseMiddleware<ExceptionMiddleware>(); // Global Exception Handling
            app.UseForwardedHeaders();

            // Always enable OpenAPI & Scalar API reference for easy testing in staging/production
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.Title = "Portfolio API";
                options.Theme = ScalarTheme.DeepSpace;
                options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
                options.Authentication = new ScalarAuthenticationOptions
                {
                    PreferredSecuritySchemes = ["Bearer"]
                };
            });

            app.UseStaticFiles();
            app.UseCors("AllowPortfolioClients");

            app.UseAuthentication();
            app.UseAuthorization();

            // Root & Health check endpoints (supports both GET and HEAD for cloud health probes)
            app.MapMethods("/", ["GET", "HEAD"], () => Results.Ok(new { status = "healthy", message = "Portfolio API is live", docs = "/scalar/v1" }));
            app.MapMethods("/health", ["GET", "HEAD"], () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

            app.MapControllers();

            // 9. Auto-Migrate and Seed Database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    Log.Information("Applying EF Core migrations if any...");
                    await context.Database.MigrateAsync();

                    var seeder = services.GetRequiredService<DataSeeder>();
                    Log.Information("Seeding portfolio initial data...");
                    await seeder.SeedAsync();
                    Log.Information("Portfolio database successfully initialized and seeded!");
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "Database migration/seed skipped or encountered an error.");
                }
            }

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Portfolio API application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
