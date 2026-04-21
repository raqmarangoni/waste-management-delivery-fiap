# WasteManagement API - Entrega Final (SQLite)

Tema: Gestão de resíduos e reciclagem
Framework: ASP.NET Core 8 (.NET 8)
Banco: SQLite

## O que está incluso
- API com arquitetura simples (Models, ViewModels, Services, Repositories, Controllers)
- Endpoints:
  - GET /api/collections?page=1&pageSize=10
  - GET /api/collections/{id}
  - POST /api/collections
  - GET /api/alerts
  - POST /api/alerts
  - GET /api/reports/summary
  - POST /api/sensors/telemetry
- Testes xUnit (um teste por controller com verificação de status 200)
- Dockerfile
- Script SQL em `migrations/create_tables.sql`
- Postman collection

## Como rodar
1. Verifique se você tem .NET 8 SDK: `dotnet --version` (deve retornar 8.x.x)
2. Navegue até a pasta do projeto:
   ```bash
   cd WasteManagementDelivery/src/WasteManagement.API
   ```
3. Restaurar e rodar:
   ```bash
   dotnet restore
   dotnet run
   ```
   O projeto criará automaticamente um arquivo `waste.db` na pasta do projeto (SQLite).
   A API vai ficar disponível em `https://localhost:5001` e `http://localhost:5000`.

4. Rodar testes:
   ```bash
   cd ../../tests/WasteManagement.Tests
   dotnet test
   ```

5. Para criar o DB manualmente, execute o script SQL em `migrations/create_tables.sql` usando um cliente SQLite.
