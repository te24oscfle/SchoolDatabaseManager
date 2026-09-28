
using SchoolDatabaseManager.Classes.Models;
using SchoolDatabaseManager.Commands;
using SchoolDatabaseManager.Database;
using System.Runtime.InteropServices;

namespace SchoolDatabaseManager
{
    internal class Program
    {
        static string GetStringFromArguments(string[] arguments)
        {
            return string.Join(" ", arguments);
        }

        static void PrintStudents(List<Student> students)
        {
            foreach (Student student in students)
            {
                Console.WriteLine(student.Name);
                Console.WriteLine($"    ID={student.Id}");
                Console.WriteLine($"    Student Email={student.StudentEmail}");
                Console.WriteLine($"    Group ID={student.GroupId}");
                Console.Write("\n");
            }
        }

        

        static void PrintGroups(List<Group> groups)
        {
            foreach (Group group in groups)
            {
                Console.WriteLine(group.Name);
                Console.WriteLine($"    ID={group.Id}");
                Console.Write("\n");
            }
        }

        static void Main(string[] args)
        {
            DatabaseHelper.InitilizeDatabase();

            Dictionary<string, Action<string[]>> commands = CommandManager.GetCommands();

            bool shouldExit = false;
            while (!shouldExit)
            {
                Console.Write(">> ");
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;

                string[] split = input.Split(" ");
                string commandName = split[0];
                string[] arguments = split.Skip(1).ToArray();

                if (commandName == "exit")
                {
                    shouldExit = true;
                    continue;
                }

                if (!commands.TryGetValue(commandName, out Action<string[]>? command))
                {
                    Console.WriteLine("Invalid command.");
                    continue;
                }

                try
                {
                    command(arguments);
                } 
                catch (Exception exception)
                {
                    Console.WriteLine($"Error when executing command {commandName}");
                    Console.WriteLine(exception);
                }
                
                //switch (command)
                //{
                //    // =============================
                //    // === Students
                //    // =============================
                //    case "addStudent":
                //    {
                //        if (string.IsNullOrEmpty(name))
                //        {
                //            Console.WriteLine("Invalid name.");
                //            continue;
                //        }
                //        DatabaseManager.AddStudent(new Student(name));
                //        break;
                //    }

                //    case "removeStudent":
                //    {
                //        DatabaseManager.RemoveStudent(primaryId);
                //        break;
                //    }

                //    case "getStudents":
                //    {
                //        List<Student> students = DatabaseManager.GetStudents();
                //        PrintStudents(students);
                //        break;
                //    }

                //    case "getStudentsInGroup":
                //    {
                //        List<Student> students = DatabaseManager.GetStudentsInGroup(primaryId);
                //        PrintStudents(students);
                //        break;
                //    }

                //    case "assignStudentToGroup":
                //    {
                //        DatabaseManager.AssignStudentToGroup(primaryId, secondaryId);
                //        break;
                //    }

                //    // =============================
                //    // === Teachers
                //    // =============================
                //    case "addTeacher":
                //    {
                //        if (string.IsNullOrEmpty(name))
                //        {
                //            Console.WriteLine("Invalid name.");
                //            continue;
                //        }
                //        DatabaseManager.AddTeacher(new Teacher(name));
                //        break;
                //    }

                //    case "removeTeacher":
                //    {
                //        DatabaseManager.RemoveTeacher(primaryId);
                //        break;
                //    }

                //    case "assignTeacherToGroup":
                //    {
                //        DatabaseManager.AssignTeacherToGroup(primaryId, secondaryId);
                //        break;
                //    }

                //    case "getTeachers":
                //    {
                //        List<Teacher> teachers = DatabaseManager.GetTeachers();
                //        PrintTeachers(teachers);
                //        break;
                //    }

                //    case "getTeachersInGroup":
                //    {
                //        List<Teacher> teachers = DatabaseManager.GetTeachersInGroup(primaryId);
                //        PrintTeachers(teachers);
                //        break;
                //    }

                //    // =============================
                //    // === Groups
                //    // =============================
                //    case "addGroup":
                //    {
                //        if (string.IsNullOrEmpty(name))
                //        {
                //            Console.WriteLine("Invalid name.");
                //            continue;
                //        }
                //        DatabaseManager.AddGroup(new Group(name));
                //        break;
                //    }
                       
                //    case "getGroups":
                //    {
                //        List<Group> groups = DatabaseManager.GetGroups();
                //        PrintGroups(groups);
                //        break;
                //    }

                //    // =============================
                //    // === Misc
                //    // =============================

                //    case "exit":
                //    {
                //        shouldExit = true;
                //        break;
                //    }

                //    default:
                //    {
                //        Console.WriteLine("Invalid command.");
                //        break;
                //    }   
                //}
            }
        }
    }
}
