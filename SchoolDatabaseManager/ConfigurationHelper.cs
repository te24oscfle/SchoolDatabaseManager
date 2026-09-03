using Microsoft.Extensions.Configuration;

namespace SchoolDatabaseManager
{
    public static class ConfigurationHelper
    {
        private static readonly IConfiguration _configuration;

        static ConfigurationHelper() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static string GetConnectionString(string name)
        {
            string? connectionString = _configuration.GetConnectionString(name);
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException($"Could not find a conncetion string named {name}");

            return connectionString;
        }
    }
}
