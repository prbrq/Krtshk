using System.Data;

using Dapper;

using Microsoft.Data.Sqlite;

namespace Krtshk.Models;

public interface IDatabaseContext
{
    IDbConnection CreateConnection();

    void InitializeDatabase();
}

public class DatabaseContext(IConfiguration configuration) : IDatabaseContext
{
    public IDbConnection CreateConnection()
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        return new SqliteConnection(connectionString);
    }

    public void InitializeDatabase()
    {
        using var connection = CreateConnection();

        connection.Open();

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Links (
                Key TEXT PRIMARY KEY,
                Url TEXT
            );
        ");
    }
}