using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SchoolDatabaseManager.Classes.Models
{
    public class Student
    {
        public int Id;
        public string Name;
        public string StudentEmail;

        public Student(string name)
        {
            Id = -1;
            Name = name;
            StudentEmail = GetStudentEmail();
        }

        private string GetStudentEmail()
        {
            string processedName = Name.ToLower()
                .Replace("å", "a")
                .Replace("ä", "a")
                .Replace("ö", "o"); // I should be doing something more robust here but this works
            return $"{Regex.Replace(processedName, @"\s+", ".").ToLower()}@falufri.se";
        }

        public override string ToString()
        {
            return $"{Name} | {StudentEmail}";
        }
    }
}
