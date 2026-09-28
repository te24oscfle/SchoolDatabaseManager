using SchoolDatabaseManager.Classes.Models;
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
    }
}
