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
        public int GroupId;

        public Student(string name)
        {
            Id = -1;
            Name = name;
            StudentEmail = GetStudentEmail();
            GroupId = -1;
        }

        public Student(int id, string name, string studentEmail, int groupId)
        {
            Id = id;
            Name = name;
            StudentEmail = studentEmail;
            GroupId = groupId;
        }

        private string GetStudentEmail()
        {
            string processedName = Name.ToLower()
                .Replace("å", "a")
                .Replace("ä", "a")
                .Replace("ö", "o"); // I should be doing something more robust here but this works
            string studentEmail = $"{Regex.Replace(processedName, @"\s+", ".").ToLower()}@falufri.se";
            return studentEmail;
        }
        
        public void GenerateStudentEmail()
        {
            StudentEmail = GetStudentEmail();
        }

        public override string ToString()
        {
            return $"{Name} | {StudentEmail}";
        }
    }
}
