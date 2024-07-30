#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Communication.API/Communication.API.csproj", "Communication.API/"]
COPY ["Communication.Application/Communication.Application.csproj", "Communication.Application/"]
COPY ["Communication.Domain/Communication.Domain.csproj", "Communication.Domain/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Caching/Caching.csproj", "Caching/"]
COPY ["Observability/Observability.csproj", "Observability/"]
COPY ["ServiceBus/ServiceBus.csproj", "ServiceBus/"]
COPY ["Communication.Persistence/Communication.Infrastructure.csproj", "Communication.Persistence/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "./Communication.API/Communication.API.csproj"
COPY . .
WORKDIR "/src/Communication.API"
RUN dotnet build "./Communication.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Communication.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Communication.API.dll"]