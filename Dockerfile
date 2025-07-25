##
## Dockerfile for the NetFullStack portfolio project
##
## This multi‑stage build first restores and publishes the ASP.NET Core
## Web API project and then packages it into a lightweight runtime
## environment.  See docker-compose.yml for orchestrating the API with a
## SQL Server container.

# Stage 1: build and publish the API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY backend/ ./backend/
COPY backend/NetFullStack.API.csproj ./backend/

# Restore dependencies
RUN dotnet restore "backend/NetFullStack.API.csproj"

# Publish the API to a self‑contained folder
RUN dotnet publish "backend/NetFullStack.API.csproj" -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 8080 (the default for ASP.NET apps when hosted in a container)
EXPOSE 8080

# Start the API
ENTRYPOINT ["dotnet", "NetFullStack.API.dll"]