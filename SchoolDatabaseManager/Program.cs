

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
                        DatabaseManager.AddStudent(new Student(string.Join(" ", arguments)));
                        break;
                    }

                    case "getStudents":
                    {
                        List<Student> students = DatabaseManager.GetStudents();
                        foreach(Student student in students)
                        {
                            Console.WriteLine(student.Name);
                            Console.WriteLine($"    ID={student.Id}");
                            Console.WriteLine($"    Student Email={student.StudentEmail}");
                            Console.WriteLine($"    Group ID={student.GroupId}");
                            Console.Write("\n");
                        }
                            
                        break;
                    }

                    case "getStudentsInGroup":
                    {
                        int.TryParse(arguments[0], out int groupId);
                        List<Student> students = DatabaseManager.GetStudentsInGroup(groupId);
                        foreach (Student student in students)
                        {
                            Console.WriteLine(student.Name);
                            Console.WriteLine($"    ID={student.Id}");
                            Console.WriteLine($"    Student Email={student.StudentEmail}");
                            Console.Write("\n");
                        }

                        break;
                    }

                    case "assignStudentToGroup":
                        {
                            int.TryParse(arguments[0], out int studentId);
                            int.TryParse(arguments[1], out int groupId);
                            DatabaseManager.AssignStudentToGroup(studentId, groupId);
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
                        DatabaseManager.AddTeacher(new Teacher(string.Join(" ", arguments)));
                        break;
                    }

                    case "removeTeacher":
                    {
                        int.TryParse(arguments[0], out int teacherId);
                        DatabaseManager.RemoveTeacher(teacherId);
                        break;
                    }

                    case "getTeachers":
                    {
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

                        Group group = new Group(name);
                        DatabaseManager.AddGroup(group);

                        break;
                    }
                       
                    case "getGroups":
                    {
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
