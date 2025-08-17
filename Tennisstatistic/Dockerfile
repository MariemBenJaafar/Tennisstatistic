
# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier le fichier csproj et restaurer les dépendances
COPY ["TennisStatistic/Tennisstatistic/TennisStatistics.Api.csproj", "TennisStatistic/"]
RUN dotnet restore "TennisStatistic/TennisStatistics.Api.csproj"

# Copier le reste du projet et publier
COPY . .
WORKDIR "/src/TennisStatistic"
RUN dotnet publish "TennisStatistics.Api.csproj" -c Release -o /app/publish

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "TennisStatistics.Api.dll"]
