

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
                    {
                        string name = string.Join(" ", arguments);
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Invalid name.");
                            continue;
                        }

                        CommandHelpers.Add(students, args => new Student(string.Join(" ", args)), arguments);
                        break;
                    }

                    case "getStudents":
                    {
                        CommandHelpers.Get(students);
                        break;
                    }

                    case "addTeacher":
                    {
                        string name = string.Join(" ", arguments);
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Invalid name.");
                            continue;
                        }

                        CommandHelpers.Add(teachers, args => new Teacher(string.Join(" ", args)), arguments);
                        break;
                    }

                    case "getTeachers":
                    {
                        CommandHelpers.Get(teachers);
                        break;
                    } 
                    

                    case "addGroup":
                    {
                        string name = string.Join(" ", arguments);
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Invalid name.");
                            continue;
                        }

                        CommandHelpers.Add(groups, args => new Group(string.Join(" ", args)), arguments);
                        break;
                    }
                       
                    case "getGroups":
                    {
                        CommandHelpers.Get(groups);
                        break;
                    }
                       

                    case "exit":
                    {
                        shouldExit = true;
                        break;
                    }

                    default:
                    {
                        Console.WriteLine("Invalid command.");
                        break;
                    }
                        
                }
            }
        }
    }
}
