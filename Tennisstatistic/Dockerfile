
# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Forcer le dossier Linux pour NuGet
ENV NUGET_PACKAGES=/root/.nuget/packages

# Copier le fichier csproj
COPY ["Tennisstatistic/TennisStatistics.Api.csproj", "Tennisstatistic/"]

# Restaurer les dépendances
RUN dotnet restore "Tennisstatistic/TennisStatistics.Api.csproj"

# Copier tout le code
COPY . .

# Publier le projet
WORKDIR "/src/Tennisstatistic"
RUN dotnet publish "TennisStatistics.Api.csproj" -c Release -o /app/publish

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000
ENTRYPOINT ["dotnet", "TennisStatistics.Api.dll"]
