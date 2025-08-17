
# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier le fichier csproj avec le bon chemin
COPY ["Tennisstatistic/TennisStatistics.Api.csproj", "Tennisstatistic/"]

# Restaurer les dépendances (évite l'erreur de fallback Windows)
RUN dotnet restore "Tennisstatistic/TennisStatistics.Api.csproj" --disable-parallel

# Copier tout le code
COPY . .

# Publier le projet (sans refaire le restore)
WORKDIR "/src/Tennisstatistic"
RUN dotnet publish "TennisStatistics.Api.csproj" -c Release -o /app/publish --no-restore

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exposer le port
EXPOSE 5000

ENTRYPOINT ["dotnet", "TennisStatistics.Api.dll"]
