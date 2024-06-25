FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PDSBuddy/PDSBuddy.csproj", "ConsoleApp/"]
RUN dotnet restore "./PDSBuddy/PDSBuddy.csproj"
COPY . .
WORKDIR "/src/PDSBuddy"
RUN dotnet build "./PDSBuddy.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PDSBuddy.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PDSBuddy.dll"]