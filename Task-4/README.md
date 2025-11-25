<h1 align="center">Завдання №4</h1>

## Умова
- Створити два сервіси:
   - ``backend`` (Python Flask),
   - ``db`` (PostgreSQL).
- Написати ``docker-compose.yml``
- Запустити:
``docker compose up``
## Виконання
### Program.cs
Для прикладу було створено простий ***C# .NET 9 Web API*** для роботи з базою даних ***PostgreSQL***.
```csharp
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/users", async () =>
{
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    var cmd = new NpgsqlCommand("SELECT id, name FROM users", conn);
    var reader = await cmd.ExecuteReaderAsync();

    var users = new List<object>();
    
    while (await reader.ReadAsync())
    {
        users.Add(new
        {
            id = reader.GetInt32(0),
            Name = reader.GetString(1)
        });
    }
    return users;
});

app.Run();
```
***Get /users*** повертає список користувачів у форматі ***JSON***.
### Dockerfile
```Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "backend.dll"]
```
### docker-compose.yml
```docker-compose.yml
services:
  backend:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "8080:8080"
    container_name: notes_backend
    depends_on:
      - db
  db:
    image: docker.io/library/postgres:15
    ports:
      - "5432:5432"
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=1234
    volumes:
      - db_data:/var/lib/postgresql/data
volumes:
  db_data:
```
### Налаштування підключення до БД 
```json
"ConnectionStrings": {
   "Default": "Host=db;Port=5432;Database=backend_db_1;Username=postgres;Password=1234"
}
```
### Запуск
![](https://github.com/user-attachments/assets/bb74559d-2feb-49d6-92b1-9df4a219dbef)







