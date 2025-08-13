# ProjectLog Backend

**Demo Link Frontend**: [https://red-pebble-0a7c38403.2.azurestaticapps.net/](https://red-pebble-0a7c38403.2.azurestaticapps.net/)
**Demo Link Backend**: [https://projectlog-api.thankfulsky-655b2f40.westeurope.azurecontainerapps.io/swagger/index.html](https://projectlog-api.thankfulsky-655b2f40.westeurope.azurecontainerapps.io/swagger/index.html)

***Note: any changes made on this website are publicly visible.***

## Overview

**ProjectLog Backend** is a scalable, dynamic, and cloud-ready REST API designed as the backend service for the ProjectLog full-stack sample application. This project demonstrates modern cloud-native development practices with multi-environment support, including local development and cloud deployments.

ProjectLog allows users to track past projects and add notes to them.

---

## Technology Stack
- **Language & Framework:** C# / .NET 9.0 / ASP.NET Core 9.0 / Entity Framework Core 9.0  
- **Containerization:** Docker  
- **Orchestration:** Docker Compose for local development environment management  
- **Documentation:** Auto-generated OpenAPI visualised with Swagger
---

## Supported Environments

| Environment         | Description                                                                                 | Database             |
|---------------------|---------------------------------------------------------------------------------------------|----------------------|
| **Local**           | Development environment using containerized MSSQL server with automatic DB initialization and seeded dummy data. Ideal for rapid iteration and testing. | MSSQL Server Container |
| **Small Scale Cloud**| Lightweight Azure deployment utilizing Azure SQL for cost-effective cloud hosting.          | Azure SQL Database   |
| **Global Cloud**    | Fully managed, globally distributed cloud environment leveraging Azure Cosmos DB for scalability and high availability. | Azure Cosmos DB       |

> **Note:** In production, database migrations should be handled via CI/CD pipelines to ensure schema consistency.

---

## Prerequisites

- Git  
- Docker with Docker Compose Plugin (Docker Desktop for Windows recommended)  
- .NET 9.0 SDK  
- Visual Studio 2022 (Optional but highly recommended)

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/Ikaron/project-log-backend.git
cd project-log-backend/ProjectLog.Api
```

### 2. Create Environment Variable Files

These must be created in `ProjectLog.Api/`

.env
> ```env
> SA_PASSWORD=YourStrongMSSQLPassword
> ```

ProjectLog.Api.Dev.env
> ```env
> ConnectionStrings__DefaultSqlConnection=Server=mssql,1433;Database=ProjectLog;User Id=sa;Password=${SA_PASSWORD};TrustServerCertificate=True;
> ```

ProjectLog.Api.env
> If not using Cosmos DB, this can be left empty
> ```env
> COSMOS__ENABLED=false
> COSMOS__ENDPOINT=https://YourCosmosEndpoint:8081
> COSMOS__CONTAINER=projectlog-db
> COSMOS__ACCOUNTKEY=YourCosmosAccountKey
> ```

### 3. Other Settings
Ports can be changed in `ProjectLog.Api/docker-compose.override.yml`

---

## Usage

### Visual Studio
Open the solution file `(ProjectLog.Api/ProjectLog.Api.sln)` in Visual Studio, select Docker Compose as the run target, and start debugging.

### Command Line
Build and start services:
```bash
docker compose build
docker compose up
```

---

### Query
Use your favourite HTTP query engine:

> **Swagger** (Recommended)
> 
> This project uses Swagger to automatically generate an API explorer front-end.
>
> Navigate to [https://localhost:8081/swagger/](https://localhost:8081/swagger/)
> 
> View the end-points of interest and run queries directly in your browser!


> **curl**
> ```bash
> curl https://localhost:8081/api/Projects
> ```

> **Browser**
>
> Navigate to [https://localhost:8081/api/Projects](https://localhost:8081/api/Projects)

> **Visual Studio**
>
> In the Solution Explorer, find and open `ProjectLog.Api/ProjectLog.Api/ProjectLog.Api.http`
>
> This file contains a few example queries that can be run directly in the editor.

---

## API Details
- Data Model:
  - `Projects` table - Stores information about each project
  - `Notes` table - **Coming soon!** Will store information about the notes on every project
- Documentation:
  - `OpenAPI` (auto-gen): [https://localhost:8081/openapi/v1.json](https://localhost:8081/openapi/v1.json)
  - `Swagger` (auto-gen): [https://localhost:8081/swagger/](https://localhost:8081/swagger/)

## Current Limitations
- Note functionality not yet implemented