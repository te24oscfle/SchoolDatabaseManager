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
                """
                INSERT INTO students (name, student_email)
                VALUES (@name, @studentEmail)
                RETURNING id
                """,
                connection
            );
            
            // Insert values into command
            command.Parameters.AddWithValue("name", student.Name);
            command.Parameters.AddWithValue("studentEmail", student.StudentEmail);

            // Execute the command and get the ID
            student.Id = command.ExecuteScalar() is int id ? id : -1;

            Console.WriteLine($"Added Student {student.Name} to the database. (ID={student.Id})");
        }

        public static List<Student> GetStudents()
        {
            // Open a connection
            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            // Create command
            using NpgsqlCommand command = new NpgsqlCommand(
                """
                SELECT * FROM students;
                """,
                connection
            );

            // Get reader object
            NpgsqlDataReader reader = command.ExecuteReader();

            // Read all rows and create students
            List<Student> students = new List<Student>();
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                string studentEmail = reader.GetString(reader.GetOrdinal("student_email"));

                Student student = new Student(id, name, studentEmail);
                students.Add(student);
            }

            return students;
        }
    }
}
