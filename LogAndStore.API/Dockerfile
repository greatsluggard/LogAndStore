FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LogAndStore.API/LogAndStore.API.csproj", "LogAndStore.API/"]
COPY ["LogAndStore.Application/LogAndStore.Application.csproj", "LogAndStore.Application/"]
COPY ["LogAndStore.DAL/LogAndStore.DAL.csproj", "LogAndStore.DAL/"]
COPY ["LogAndStore.Domain/LogAndStore.Domain.csproj", "LogAndStore.Domain/"]
RUN dotnet restore "./LogAndStore.API/LogAndStore.API.csproj"
COPY . .
WORKDIR "/src/LogAndStore.API"
RUN dotnet build "./LogAndStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LogAndStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogAndStore.API.dll"]