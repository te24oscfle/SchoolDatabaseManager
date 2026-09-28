using Microsoft.Data.Sqlite;
using SchoolDatabaseManager.Classes;

namespace SchoolDatabaseManager.Database
{
    public record FailedItem<T>(
        T Item,
        Exception Exception
    );
    
    public static class DatabaseHelper
    {
        private static string databasePath = "Database/database.db";
        private static string connectionString = $"Data Source={databasePath}";
        private static bool isDatabaseInitilized = false;
        
        private static SqliteConnection GetConnection()
        {
            // Create Database directory if it doesn't already exist
            Directory.CreateDirectory("Database");

            // Create and open connection
            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();
            return connection;
        }

        public static void InitilizeDatabase()
        {
            // Check if database is already initilized
            if (isDatabaseInitilized)
                return;
            
            // Open connection
            using var connection = GetConnection();

            List<SqliteCommand> commands = new List<SqliteCommand>
            {
                // Student table command
                new SqliteCommand(
                    """
                    CREATE TABLE IF NOT EXISTS students
                    (
                        id INTEGER PRIMARY KEY,
                        name TEXT NOT NULL,
                        student_email TEXT NOT NULL,
                        group_id INTEGER
                    )
                    """, connection),

                // Teacher table command
                new SqliteCommand(
                    """
                    CREATE TABLE IF NOT EXISTS teachers
                    (
                        id INTEGER PRIMARY KEY,
                        name TEXT NOT NULL,
                        teacher_email TEXT NOT NULL,
                        group_id INTEGER
                    )
                    """, connection),

                // Group table command
                new SqliteCommand(
                    """
                    CREATE TABLE IF NOT EXISTS groups
                    (
                        id INTEGER PRIMARY KEY,
                        name TEXT NOT NULL
                    )
                    """, connection)
            };

            foreach(SqliteCommand command in commands)
            {
                Console.WriteLine($"\nExecuting command: {command.CommandText}");
                command.ExecuteNonQuery();
                command.Dispose();
            }

            isDatabaseInitilized = true;
        }

        #region General Reader/Writer functions

        public static T? Read<T>(string sqlCommand, Func<SqliteDataReader, T> mapper, Action<SqliteCommand>? configureCommand=null)
        {
            // Check if database is initilized
            if (!isDatabaseInitilized)
                throw new Exception("Database must be initilized before this function can be called");
            
            // Open connection and create command
            using SqliteConnection connection = GetConnection();
            using SqliteCommand command = new SqliteCommand(sqlCommand, connection);

            // Invoke the configureCommand action only if it was provided
            configureCommand?.Invoke(command);

            // Read data
            using SqliteDataReader reader = command.ExecuteReader();
            
            // No data was found
            if (!reader.Read())
                return default;

            // Data was found
            return mapper(reader);
        }

        public static List<T> ReadToList<T>(string sqlCommand, Func<SqliteDataReader, T> mapper, Action<SqliteCommand>? configureCommand = null)
        {
            // Check if database is initilized
            if (!isDatabaseInitilized)
                throw new Exception("Database must be initilized before this function can be called");

            // Open connection and create command
            using SqliteConnection connection = GetConnection();
            using SqliteCommand command = new SqliteCommand(sqlCommand, connection);

            // Invoke the configureCommand action only if it was provided
            configureCommand?.Invoke(command);

            // Read data
            using SqliteDataReader reader = command.ExecuteReader();

            List<T> list = new List<T>();
            while (reader.Read())
            {
                list.Add(mapper(reader));
            }

            return list;
        }

        public static bool Write(string sqlCommand, Action<SqliteCommand>? configureCommand = null)
        {
            // Check if database is initilized
            if (!isDatabaseInitilized)
                throw new Exception("Database must be initilized before this function can be called");

            // Open connection and create command
            using SqliteConnection connection = GetConnection();
            using SqliteCommand command = new SqliteCommand(sqlCommand, connection);

            // Invoke the configureCommand action only if it was provided
            configureCommand?.Invoke(command);

            // Write data
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error when executing following SQL command:\n{command.CommandText}");

                Console.WriteLine($"\nParamaters:");
                foreach(SqliteParameter paramater in command.Parameters)
                {
                    Console.WriteLine($"\t{paramater.ParameterName}: {paramater.Value}");
                }

                Console.WriteLine($"\n{exception}\n");
                return false;
            }
            
        }

        public static List<FailedItem<T>> WriteFromList<T>(List<T> list, string sqlCommand, Action<SqliteCommand, T> configureCommand)
        {
            // Check if database is initilized
            if (!isDatabaseInitilized)
                throw new Exception("Database must be initilized before this function can be called");

            // Open connection and create command
            using SqliteConnection connection = GetConnection();
            using SqliteCommand command = new SqliteCommand(sqlCommand, connection);

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

        #region Class-specific read/write functions
        public static Student ReadStudent(SqliteDataReader reader)
        {
            // Create Student object
            Student student = new Student
            (
                reader.GetInt32(reader.GetOrdinal("id")),
                reader.GetString(reader.GetOrdinal("name")),
                reader.IsDBNull(reader.GetOrdinal("student_email")) is false ? reader.GetString(reader.GetOrdinal("student_email")) : "",
                reader.GetInt32(reader.GetOrdinal("group_id"))
            );

            // Generate student email if no student email was stored in database
            if (string.IsNullOrWhiteSpace(student.StudentEmail))
                student.GenerateStudentEmail();

            return student;
        }

        public static void WriteStudent(SqliteCommand command, Student student)
        {
            // Add values to command paramaters
            command.Parameters.AddWithValue("name", student.Name);
            command.Parameters.AddWithValue("student_email", student.StudentEmail);
            command.Parameters.AddWithValue("group_id", student.GroupId);
        }

        public static Teacher ReadTeacher(SqliteDataReader reader)
        {
            // Create Teacher object
            Teacher teacher = new Teacher
            (
                reader.GetInt32(reader.GetOrdinal("id")),
                reader.GetString(reader.GetOrdinal("name")),
                reader.IsDBNull(reader.GetOrdinal("teacher_email")) is false ? reader.GetString(reader.GetOrdinal("teacher_email")) : "",
                reader.GetInt32(reader.GetOrdinal("group_id"))
            );

            // Generate teacher email if no teacher email was stored in database
            if (string.IsNullOrWhiteSpace(teacher.TeacherEmail))
                teacher.GenerateTeacherEmail();

            return teacher;
        }

        public static void WriteTeacher(SqliteCommand command, Teacher teacher)
        {
            // Add values to command paramaters
            command.Parameters.AddWithValue("name", teacher.Name);
            command.Parameters.AddWithValue("teacher_email", teacher.TeacherEmail);
            command.Parameters.AddWithValue("group_id", teacher.GroupId);
        }

        public static Group ReadGroup(SqliteDataReader reader)
        {
            return new Group
            (
                reader.GetString(reader.GetOrdinal("name")),
                reader.GetInt32(reader.GetOrdinal("id"))
            );
        }

        public static void WriteGroup(SqliteCommand command, Group group)
        {
            command.Parameters.AddWithValue("name", group.Name);
        }

        #endregion
    }
}