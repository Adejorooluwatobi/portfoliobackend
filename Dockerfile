# ------------------------------------------------------------------------------
# Stage 1: Build & Publish with .NET 10 SDK
# ------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files first to leverage Docker layer caching
COPY ["Portfolio.Domain/Portfolio.Domain.csproj", "Portfolio.Domain/"]
COPY ["Portfolio.Application/Portfolio.Application.csproj", "Portfolio.Application/"]
COPY ["Portfolio.Infrastructure/Portfolio.Infrastructure.csproj", "Portfolio.Infrastructure/"]
COPY ["Portfolio.Api/Portfolio.Api.csproj", "Portfolio.Api/"]

# Restore NuGet dependencies
RUN dotnet restore "Portfolio.Api/Portfolio.Api.csproj"

# Copy the remaining project sources
COPY . .

# Build and Publish
WORKDIR "/src/Portfolio.Api"
RUN dotnet publish "Portfolio.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ------------------------------------------------------------------------------
# Stage 2: Runtime Image with ASP.NET Core 10
# ------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Install required system packages for PostgreSQL Npgsql GSSAPI/Kerberos and curl for healthcheck
RUN apt-get update && apt-get install -y --no-install-recommends \
    libgssapi-krb5-2 \
    curl \
    && rm -rf /var/lib/apt/lists/*

# Copy published application from build stage
COPY --from=build /app/publish .

# Ensure wwwroot and uploads directory exist for media attachments
RUN mkdir -p /app/wwwroot/uploads

# Expose default HTTP port
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Healthcheck to verify the API is serving requests
HEALTHCHECK --interval=30s --timeout=5s --start-period=20s --retries=3 \
    CMD curl -f http://localhost:8080/api/Articles || exit 1

ENTRYPOINT ["dotnet", "Portfolio.Api.dll"]