

using Npgsql;
using SchoolDatabaseManager.Helpers;
using SchoolDatabaseManager.Classes;
using SchoolDatabaseManager.Classes.Models;

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

        static void PrintTeachers(List<Teacher> teachers)
        {
            foreach (Teacher teacher in teachers)
            {
                Console.WriteLine(teacher.Name);
                Console.WriteLine($"    ID={teacher.Id}");
                Console.WriteLine($"    Student Email={teacher.TeacherEmail}");
                Console.WriteLine($"    Group ID={teacher.GroupId}");
                Console.Write("\n");
            }
        }

        static void Main(string[] args)
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

                string name = GetStringFromArguments(arguments);

                // primaryId and secondaryId was previously student/teacherId and groupId respectively. '
                // I moved the int.TryParse calls out of the switch cases to refactorise the code.
                // This system assums groupId is always the second argument, but some command take a group id as the first command.
                // To solve this, they are renamed to primaryId and secondaryId. This honestly sucks for readability, but 
                // everything about this sucks, so whatever.

                // Checks if arguments is greater than 0. If it is, it will try to parse the int to a temp variable
                // If the temp variable exists, primaryId will be equal to that. Otherwise, it is set to 0.
                int primaryId = arguments.Length > 0 && int.TryParse(arguments[0], out int parsedPrimaryId)
                    ? parsedPrimaryId
                    : 0;

                // Same thing.
                int secondaryId = arguments.Length > 1 && int.TryParse(arguments[1], out int parsedSecondaryId)
                    ? parsedSecondaryId
                    : 0;
                

                switch (command)
                {
                    // =============================
                    // === Students
                    // =============================
                    case "addStudent":
                    {
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Invalid name.");
                            continue;
                        }
                        DatabaseManager.AddStudent(new Student(name));
                        break;
                    }

                    case "removeStudent":
                    {
                        DatabaseManager.RemoveStudent(primaryId);
                        break;
                    }

                    case "getStudents":
                    {
                        List<Student> students = DatabaseManager.GetStudents();
                        PrintStudents(students);
                        break;
                    }

                    case "getStudentsInGroup":
                    {
                        List<Student> students = DatabaseManager.GetStudentsInGroup(primaryId);
                        PrintStudents(students);
                        break;
                    }

                    case "assignStudentToGroup":
                    {
                        DatabaseManager.AssignStudentToGroup(primaryId, secondaryId);
                        break;
                    }

                    // =============================
                    // === Teachers
                    // =============================
                    case "addTeacher":
                    {
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Invalid name.");
                            continue;
                        }
                        DatabaseManager.AddTeacher(new Teacher(name));
                        break;
                    }

                    case "removeTeacher":
                    {
                        DatabaseManager.RemoveTeacher(primaryId);
                        break;
                    }

                    case "assignTeacherToGroup":
                    {
                        DatabaseManager.AssignTeacherToGroup(primaryId, secondaryId);
                        break;
                    }

                    case "getTeachers":
                    {
                        List<Teacher> teachers = DatabaseManager.GetTeachers();
                        PrintTeachers(teachers);
                        break;
                    }

                    case "getTeachersInGroup":
                    {
                        List<Teacher> teachers = DatabaseManager.GetTeachersInGroup(primaryId);
                        PrintTeachers(teachers);
                        break;
                    }

                    // =============================
                    // === Groups
                    // =============================
                    case "addGroup":
                    {
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

                    // =============================
                    // === Misc
                    // =============================

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
