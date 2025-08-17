# Tennisstatistic
API RESTful développée en ASP.NET Core 8.0 pour gérer des statistiques de tennis.

## Stack technique

- .NET 8 — Minimal API
- FluentValidation — Validation avancée
- Serilog— Logs structurés
- xUnit + **WebApplicationFactory** — Tests unitaires et d’intégration
- Swagger / Swashbuckle— Documentation interactive
- Docker — Multi-stage pour build léger
- Render — Déploiement Cloud

## Lancer l'application localement
Clonez le dépôt :

   bash
   git clone https://github.com/MariemBenJaafar/Tennisstatistic.git
   cd Tennisstatistic

### Restaurez les dépendances :

dotnet restore

### Lancez l'application :

dotnet run --project Tennisstatistic/TennisStatistics.Api.csproj --urls http://localhost:5000

### Accédez à la documentation Swagger :

http://localhost:5000/swagger


## URL de déploiement

L'application est déployée sur Render à l'adresse :  https://tennisstatistic.onrender.com

- Documentation Swagger : https://tennisstatistic.onrender.com/swagger
- Exemple d'endpoint API (La liste de tous les joueurs) : https://tennisstatistic.onrender.com/api/players


## Fonctionnalités

### Joueurs
- GET `/api/players` — liste tous les joueurs
- GET `/api/players/{id}` — récupère un joueur par ID
- POST `/api/players` — ajoute un nouveau joueur
- PUT `/api/players/{id}` — met à jour un joueur
- DELETE `/api/players/{id}` — supprime un joueur

### Statistiques
- GET `/api/stats/best-country` — pays avec le meilleur ratio de victoires
- GET `/api/stats/average-imc` — IMC moyen des joueurs
- GET `/api/stats/median-height` — taille médiane des joueurs

## Tests

Pour lancer les tests unitaires clique droit sur le projet "TennisStatsTests" => Exécuter les tests.

## Structure de projet

Tennisstatistic/
│
├── Tennisstatistic/
│   ├── Controllers/
│   ├── Services/
│   ├── Repositories/
│   ├── DTOs/
│   ├── Models/
│   ├── Validators/
│   └── Program.cs
│
├── TennisStatsTests/
│   ├── UnitTests/
│   └── IntegrationTests/
│
├── Dockerfile
└── README.md

