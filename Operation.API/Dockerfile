#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Operation.API/Operation.API.csproj", "Operation.API/"]
COPY ["Operation.Application/Operation.Application.csproj", "Operation.Application/"]
COPY ["Operation.Domain/Operation.Domain.csproj", "Operation.Domain/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Caching/Caching.csproj", "Caching/"]
COPY ["Observability/Observability.csproj", "Observability/"]
COPY ["ServiceBus/ServiceBus.csproj", "ServiceBus/"]
COPY ["Operation.Instrastructure/Operation.Instrastructure.csproj", "Operation.Instrastructure/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "./Operation.API/Operation.API.csproj"
COPY . .
WORKDIR "/src/Operation.API"
RUN dotnet build "./Operation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Operation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Operation.API.dll"]