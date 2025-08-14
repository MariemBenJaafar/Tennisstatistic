# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier le fichier csproj et restaurer les dépendances
COPY ["TennisStatistics.Api/TennisStatistics.Api.csproj", "TennisStatistics.Api/"]
RUN dotnet restore "TennisStatistics.Api/TennisStatistics.Api.csproj"

# Copier le reste du projet et publier
COPY . .
WORKDIR "/src/TennisStatistics.Api"
RUN dotnet publish "TennisStatistics.Api.csproj" -c Release -o /app/publish

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exposer le port HTTP (Render gère l'HTTPS)
EXPOSE 5000

# Démarrage de l'application
ENTRYPOINT ["dotnet", "TennisStatistics.Api.dll"]
