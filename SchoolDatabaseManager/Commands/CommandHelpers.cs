using SchoolDatabaseManager.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDatabaseManager.Commands
{
    public static class CommandHelpers
    {
        public static void PrintStudents(List<Student> students)
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

        public static void PrintTeachers(List<Teacher> teachers)
        {
            foreach (Teacher teacher in teachers)
            {
                Console.WriteLine(teacher.Name);
                Console.WriteLine($"    ID={teacher.Id}");
                Console.WriteLine($"    Teacher Email={teacher.TeacherEmail}");
                Console.WriteLine($"    Group ID={teacher.GroupId}");
                Console.Write("\n");
            }
        }

        public static void PrintGroups(List<Group> groups)
        {
            foreach (Group group in groups)
            {
                Console.WriteLine(group.Name);
                Console.WriteLine($"    ID={group.Id}");
                Console.Write("\n");
            }
        }
    }
}
