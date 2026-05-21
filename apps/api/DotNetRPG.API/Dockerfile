FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["apps/api/DotNetRPG.API/DotNetRPG.API.csproj", "apps/api/DotNetRPG.API/"]
RUN dotnet restore "./apps/api/DotNetRPG.API/./DotNetRPG.API.csproj"
COPY . .
WORKDIR "/src/apps/api/DotNetRPG.API"
RUN dotnet build "./DotNetRPG.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DotNetRPG.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetRPG.API.dll"]