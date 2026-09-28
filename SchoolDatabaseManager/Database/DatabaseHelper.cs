using Microsoft.Data.Sqlite;

namespace SchoolDatabaseManager.Database
{
    public record FailedItem<T>(
        T Item,
        Exception Exception
    );
        
    
    public static class DatabaseHelper
    {
        private static string databaseFilePath = "Database/database.db";
        private static string connectionString = $"DataSource={databaseFilePath}";
        
        private static SqliteConnection GetConnection()
        {
            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();
            return connection;
        }

        #region General Reader/Writer functions

        public static T? Read<T>(string sqlCommand, Func<SqliteDataReader, T> mapper, Action<SqliteCommand>? configureCommand=null)
        {
            // Open connection and create command
            using SqliteConnection connection = GetConnection();
            using SqliteCommand command = new SqliteCommand(sqlCommand);

            // Invoke the configureCommand action only if it was provided
            configureCommand!.Invoke(command);

            // Read data
            SqliteDataReader reader = command.ExecuteReader();
            
            // No data was found
            if (!reader.Read())
                return default;

            // Data was found
            return mapper(reader);
        }

        public static List<T> ReadToList<T>(string sqlCommand, Func<SqliteDataReader, T> mapper, Action<SqliteCommand>? configureCommand = null)
        {
            // Open connection and create command
            using SqliteConnection connection = GetConnection();
            using SqliteCommand command = new SqliteCommand(sqlCommand);

            // Invoke the configureCommand action only if it was provided
            configureCommand!.Invoke(command);

            // Read data
            SqliteDataReader reader = command.ExecuteReader();

            List<T> list = new List<T>();
            while (reader.Read())
            {
                list.Add(mapper(reader)));
            }

            return list;
        }

        public static void Write(string sqlCommand, Action<SqliteCommand>? configureCommand = null)
        {
            // Open connection and create command
            using SqliteConnection connection = GetConnection();
            using SqliteCommand command = new SqliteCommand(sqlCommand);

            // Invoke the configureCommand action only if it was provided
            configureCommand!.Invoke(command);

            // Write data
            command.ExecuteNonQuery();
        }

        public static List<FailedItem<T>> WriteFromList<T>(List<T> list, string sqlCommand, Action<SqliteCommand, T> configureCommand)
        {
            // Open connection and create command
            using SqliteConnection connection = GetConnection();
            using SqliteCommand command = new SqliteCommand(sqlCommand);

            List<FailedItem<T>> failedItems = new List<FailedItem<T>>();
            foreach(T item in list)
            {
                try
                {
                    // Configure command
                    command.Parameters.Clear();
                    configureCommand(command, item);

                    // Write data to database
                    command.ExecuteNonQuery();

                } catch (Exception exception)
                {
                    // Something went wrong when writing item to database
                    failedItems.Add(new FailedItem<T>(item, exception));
                }
            }

            return failedItems;
        }

        #endregion
    }
}