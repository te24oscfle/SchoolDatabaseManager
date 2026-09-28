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
                // Student Commands
                {"addStudent", AddStudent },
                {"removeStudent", RemoveStudent },
                {"assignStudentToGroup", AssignStudentToGroup },
                {"getStudents", GetStudents },
                {"getStudentsInGroup", GetStudentsInGroup },

                // Teacher Commands
                {"addTeacher", AddTeacher },
                {"removeTeacher", RemoveTeacher },
                {"assignTeacherToGroup", AssignTeacherToGroup },
                {"getTeachers", GetTeachers },
                {"getTeachersInGroup", GetTeachersInGroup },

                // Group Commands
                {"addGroup", AddGroup },
                {"removeGroup", RemoveGroup },
                {"getGroups", GetGroups }
            };
        }

        #region Student Commands

        public static void AddStudent(string[] arguments)
        {
            string name = string.Join(" ", arguments);
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("No name was provided.");
                return;
            }
            DatabaseManager.AddStudent(new Student(name));
        }

        public static void RemoveStudent(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                Console.WriteLine("No Student ID was provided.");
                return;
            }
                
            int.TryParse(arguments[0], out int studentId);
            DatabaseManager.RemoveStudent(studentId);
        }

        public static void AssignStudentToGroup(string[] arguments)
        {
            if (arguments.Length < 1)
            {
                Console.WriteLine("No Student ID was provided.");
                return;
            }

            if (arguments.Length < 2)
            {
                Console.WriteLine("No Group ID was provided.");
                return;
            }

            int.TryParse(arguments[0], out int studentId);
            int.TryParse(arguments[1], out int groupId);

            DatabaseManager.AssignStudentToGroup(studentId, groupId);
        }

        public static void GetStudents(string[] arguments) 
        {
            List<Student> students = DatabaseManager.GetStudents();
            CommandHelpers.PrintStudents(students);
        }

        public static void GetStudentsInGroup(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                Console.WriteLine("No Group ID was provided.");
                return;
            }

            int.TryParse(arguments[0], out int groupId);

            List<Student> students = DatabaseManager.GetStudentsInGroup(groupId);
            CommandHelpers.PrintStudents(students);
        }

        #endregion

        #region Teacher Commands

        public static void AddTeacher(string[] arguments)
        {
            string name = string.Join(" ", arguments);
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("No name was provided.");
                return;
            }
            DatabaseManager.AddTeacher(new Teacher(name));
        }

        public static void RemoveTeacher(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                Console.WriteLine("No Teacher ID was provided.");
                return;
            }

            int.TryParse(arguments[0], out int teacherId);
            DatabaseManager.RemoveTeacher(teacherId);
        }

        public static void AssignTeacherToGroup(string[] arguments)
        {
            if (arguments.Length < 1)
            {
                Console.WriteLine("No Teacher ID was provided.");
                return;
            }

            if (arguments.Length < 2)
            {
                Console.WriteLine("No Group ID was provided.");
                return;
            }

            int.TryParse(arguments[0], out int teacherId);
            int.TryParse(arguments[1], out int groupId);

            DatabaseManager.AssignTeacherToGroup(teacherId, groupId);
        }

        public static void GetTeachers(string[] arguments)
        {
            List<Teacher> teachers = DatabaseManager.GetTeachers();
            CommandHelpers.PrintTeachers(teachers);
        }

        public static void GetTeachersInGroup(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                Console.WriteLine("No Group ID was provided.");
                return;
            }

            int.TryParse(arguments[0], out int groupId);

            List<Teacher> teachers = DatabaseManager.GetTeachersInGroup(groupId);
            CommandHelpers.PrintTeachers(teachers);
        }

        #endregion

        #region Group Commands

        public static void AddGroup(string[] arguments)
        {
            string name = string.Join(" ", arguments);
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("No name was provided.");
                return;
            }
            DatabaseManager.AddGroup(new Group(name));
        }

        public static void RemoveGroup(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                Console.WriteLine("No Group ID was provided.");
                return;
            }

            int.TryParse(arguments[0], out int groupId);
            DatabaseManager.RemoveGroup(groupId);
        }

        public static void GetGroups(string[] arguments)
        {
            List<Group> groups = DatabaseManager.GetGroups();
            CommandHelpers.PrintGroups(groups);
        }

        #endregion
    }
}
