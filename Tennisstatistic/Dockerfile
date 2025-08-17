
# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copier les fichiers csproj et restaurer les dépendances
COPY Tennisstatistic/*.csproj ./Tennisstatistic/
RUN dotnet restore Tennisstatistic/TennisStatistics.Api.csproj

# Copier le reste des fichiers
COPY . .

# Publier l'application en mode Release
RUN dotnet publish Tennisstatistic/TennisStatistics.Api.csproj -c Release -o out

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "TennisStatistics.Api.dll"]
