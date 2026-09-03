

using Npgsql;

namespace SchoolDatabaseManager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");

            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            Console.WriteLine($"PostgreSQL Version: {connection.PostgreSqlVersion}");
        }
    }
}
