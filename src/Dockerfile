FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/HealthCheckSites/HealthCheckSites.csproj", "src/HealthCheckSites/"]

RUN dotnet restore "src/HealthCheckSites/HealthCheckSites.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "src/HealthCheckSites/HealthCheckSites.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/HealthCheckSites/HealthCheckSites.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthCheckSites.dll"]

# docker build -t health-check-sites .
# docker run --rm -d -p 5000:80 --name health-check-sites health-check-sites