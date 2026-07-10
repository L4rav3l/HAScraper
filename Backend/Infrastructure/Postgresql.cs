using Npgsql;

namespace HAScraper.Infrastructure;

public class Postgresql
{
    private readonly string _connectionString;

    public Postgresql()
    {
        _connectionString = Environment.GetEnvironmentVariable("PSQL_CONNECTION");
    }

    public async Task<NpgsqlConnection> GetOpenConnectionAsync()
    {
        var conn = new NpgsqlConnection(_connectionString);

        await conn.OpenAsync();
        return conn;
    }
}