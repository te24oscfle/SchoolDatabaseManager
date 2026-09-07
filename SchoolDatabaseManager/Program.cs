

using Npgsql;
using SchoolDatabaseManager.Helpers;
using SchoolDatabaseManager.Classes;
using SchoolDatabaseManager.Classes.Models;

namespace SchoolDatabaseManager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Student student = new Student("Oscar F");
            Teacher teacher = new Teacher("Sebastian L");

            Group te24 = new Group("TE24");

            te24.AddStudent(student);
            te24.AddTeacher(teacher);

            te24.RemoveStudent("Oscar F");

            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");

            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            Console.WriteLine($"PostgreSQL Version: {connection.PostgreSqlVersion}");
        }
    }
}
