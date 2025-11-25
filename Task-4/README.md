<h1 align="center">Завдання №4</h1>

## Умова
- Створити два сервіси:
   - ``backend`` (Python Flask),
   - ``db`` (PostgreSQL).
- Написати ``docker-compose.yml``
- Запустити:
``docker compose up``
## Виконання
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
***Get /users*** повертає список користувачів у форматі ***JSON***
<p align="center">
<img src="https://github.com/user-attachments/assets/a8fef10d-c7f5-4b26-b26f-cb93fd9096db">
</p>


