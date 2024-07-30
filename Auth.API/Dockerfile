#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Auth.API/Auth.API.csproj", "Auth.API/"]
COPY ["Auth.Application/Auth.Application.csproj", "Auth.Application/"]
COPY ["Auth.Domain/Auth.Domain.csproj", "Auth.Domain/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Caching/Caching.csproj", "Caching/"]
COPY ["Observability/Observability.csproj", "Observability/"]
COPY ["ServiceBus/ServiceBus.csproj", "ServiceBus/"]
COPY ["Auth.Persistence/Auth.Infrastructure.csproj", "Auth.Persistence/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "./Auth.API/Auth.API.csproj"
COPY . .
WORKDIR "/src/Auth.API"
RUN dotnet build "./Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.API.dll"]