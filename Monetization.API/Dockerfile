#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Monetization.API/Monetization.API.csproj", "Monetization.API/"]
COPY ["Monetization.Application/Monetization.Application.csproj", "Monetization.Application/"]
COPY ["Monetization.Domain/Monetization.Domain.csproj", "Monetization.Domain/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Caching/Caching.csproj", "Caching/"]
COPY ["Observability/Observability.csproj", "Observability/"]
COPY ["ServiceBus/ServiceBus.csproj", "ServiceBus/"]
COPY ["Monetization.Infrastructure/Monetization.Infrastructure.csproj", "Monetization.Infrastructure/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "./Monetization.API/Monetization.API.csproj"
COPY . .
WORKDIR "/src/Monetization.API"
RUN dotnet build "./Monetization.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Monetization.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Monetization.API.dll"]