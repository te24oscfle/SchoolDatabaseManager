

using Npgsql;
using SchoolDatabaseManager.Helpers;
using SchoolDatabaseManager.Classes;
using SchoolDatabaseManager.Classes.Models;

namespace SchoolDatabaseManager
{
    internal class Program
    {
        /*static async Task Main(string[] args)
        {
            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");

            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            Console.WriteLine($"PostgreSQL Version: {connection.PostgreSqlVersion}");
        }*/

        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            List<Group> groups = new List<Group>();
             
            bool shouldExit = false;
            while (!shouldExit)
            {
                Console.Write(">> ");
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;

                string[] split = input.Split(" ");
                string command = split[0];
                string[] arguments = split.Skip(1).ToArray();

                switch(command)
                {
                    case "addStudent":
                        string name = string.Join(" ", arguments);
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Invalid name.");
                            continue;
                        }

                        students.Add(new Student(name));
                        Console.WriteLine($"Added Student {name}");
                        break;

                    case "getStudents":
                        foreach (Student student in students)
                        {
                            Console.WriteLine($"{student.name} | {student.studentEmail}");
                        }
                        break;

                    case "exit":
                        shouldExit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }
    }
}
