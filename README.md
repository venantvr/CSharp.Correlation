# CSharp.Correlation

Middleware de corrélation d'identifiants pour le suivi de requêtes à travers une architecture distribuée .NET.

## Principe

Propage un identifiant unique (Correlation ID) à travers les couches d'une application WebApi, permettant de tracer une requête de bout en bout dans les logs, y compris à travers les appels inter-services via MassTransit.

## Structure

- `Contextable/` — Bibliothèque principale
  - `Correlation.cs` — Gestion du Correlation ID
  - `Context.cs` — Contexte partagé entre couches
  - `Attributes/CorrelationFilterAttribute.cs` — Filtre WebApi pour injection automatique
  - `Attributes/LoggingFilterAttribute.cs` — Filtre de logging avec corrélation
  - `Tools/MyHttpClient.cs` — HttpClient propagant le Correlation ID
- `CustomWebApi/` — WebApi de démonstration avec handlers personnalisés
- `CustomMassTransit/` — Publisher/Subscriber MassTransit avec corrélation
- `Contracts/` — Messages partagés (YourMessage1, YourMessage2)
- `Business/` — Couche métier avec handlers
- `Tests.Contextable/` — Tests unitaires

## Stack

C# / .NET Framework / WebApi / MassTransit / RabbitMQ
