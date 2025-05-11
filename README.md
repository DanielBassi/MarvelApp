
# Marvel Web Application - Backend (.NET 8)

## Requisitos Previos
- .NET SDK 8.0 o superior
- SQL Server

## Instalación
```bash
git clone https://github.com/DanielBassi/MarvelApp.git
cd MarvelApp
dotnet restore
dotnet ef database update
dotnet run
```

## Configuración
Crear o editar `appsettings.Development.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "default": "Server=localhost\\SQLEXPRESS;Database=TECNOFACTORYDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Services": {
    "marvel": {
      "url": "https://gateway.marvel.com/v1/public/",
      "publicKey": "71f849b72b1fe08753a9dbc2c609767e",
      "privateKey": "c25e172af57fbfdccefced04c626c6cfe9d7003e"
    }
  },
  "Jwt": {
      "Key": "clave-super-secreta-muy-larga-y-aleatoria",
      "Issuer": "TuApp",
      "Audience": "TuAppUsuarios",
      "ExpiresInMinutes": 120
    }
}
```

##  Arquitectura
- **DDD (Domain Driven Design)**
- **CQRS con MediatR**
- **FluentValidation** para validaciones
- **Entity Framework Core** para persistencia
- **Value Objects** para representar conceptos como Email y Identificación

```bash
src/
  Application/
    Commands/
    DTOs/
    Ports/
    Queries/
  Domain/
    Entities/
    Ports/
    ValueOBjects/
  Infrastructure/
    Adapters/
    Migrations/
    Persistence/
    Settings/
  WebAPI/
    Controllers/
    Extensions/
```

## Funcionalidades
- Registro y login de usuarios (JWT)
- Listado y detalle de cómics (API de Marvel)
- Lista de favoritos por usuario

## Buenas Prácticas
- Separación por capas
- Validaciones centralizadas
- Inyección de dependencias
- Manejo de errores global
---
