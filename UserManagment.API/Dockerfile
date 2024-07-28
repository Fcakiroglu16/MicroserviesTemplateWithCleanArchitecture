#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UserManagment.API/UserManagment.API.csproj", "UserManagment.API/"]
COPY ["UserManagment.Application/UserManagment.Application.csproj", "UserManagment.Application/"]
COPY ["UserManagment.Domain/UserManagment.Domain.csproj", "UserManagment.Domain/"]
COPY ["UserManagment.Insfrastructure/UserManagment.Insfrastructure.csproj", "UserManagment.Insfrastructure/"]
COPY ["ServiceBus/ServiceBus.csproj", "ServiceBus/"]
RUN dotnet restore "./UserManagment.API/UserManagment.API.csproj"
COPY . .
WORKDIR "/src/UserManagment.API"
RUN dotnet build "./UserManagment.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserManagment.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagment.API.dll"]