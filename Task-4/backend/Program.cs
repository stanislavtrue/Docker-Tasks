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
