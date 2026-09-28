using SchoolDatabaseManager.Classes.Models;
using SchoolDatabaseManager.Database;

namespace SchoolDatabaseManager.Commands
{
    public static class CommandManager
    {
        public static Dictionary<string, Action<string[]>> GetCommands()
        {
            return new Dictionary<string, Action<string[]>>
            {
                {"addStudent", AddStudent }

            };
        }

        public static void AddStudent(string[] arguments)
        {
            string name = string.Join(" ", arguments);
            DatabaseManager.AddStudent(new Student(name));
        }

        public static void GetStudents(string[] arguments) 
        {
            List<Student> students = DatabaseManager.GetStudents();
            foreach (Student student in students)
            {
                Console.WriteLine(student.Name);
                Console.WriteLine($"    ID={student.Id}");
                Console.WriteLine($"    Student Email={student.StudentEmail}");
                Console.WriteLine($"    Group ID={student.GroupId}");
                Console.Write("\n");
            }
        }
    }
}
