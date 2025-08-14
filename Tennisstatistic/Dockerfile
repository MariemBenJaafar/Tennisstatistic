
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TennisStatistics.Api.csproj", "./"]
RUN dotnet restore "./TennisStatistics.Api.csproj"
COPY . .
RUN dotnet publish "TennisStatistics.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TennisStatistics.Api.dll"]
