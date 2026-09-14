using Npgsql;
using SchoolDatabaseManager.Helpers;
using SchoolDatabaseManager.Classes;
using SchoolDatabaseManager.Classes.Models;

namespace SchoolDatabaseManager.Helpers
{
    public static class DatabaseManager
    {
        public static void AddStudent(Student student)
        {
            // Open a connection
            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            // Create the command
            using NpgsqlCommand command = new NpgsqlCommand(
                "INSERT INTO students (name, student_email) VALUES (@name, @studentEmail)",
                connection
            );

            command.Parameters.AddWithValue("name", student.Name); // Insert name into command
            command.Parameters.AddWithValue("studentEmail", student.StudentEmail); // Insert studentEmail into command

            // Execute the command
            command.ExecuteNonQuery();
            Console.WriteLine($"Added Student {student.Name} to the database.");
        }
    }
}
