using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SchoolDatabaseManager.Classes.Models
{   
    public class Teacher
    {
        public string Name;
        public string TeacherEmail;

        public Teacher(string name)
        {
            Name = name;
            TeacherEmail = GetTeacherEmail();
        }

        private string GetTeacherEmail()
        {
            string processedName = Name.ToLower()
                .Replace("å", "a")
                .Replace("ä", "a")
                .Replace("ö", "o"); // I should be doing something more robust here but this works
            return $"{Regex.Replace(processedName, @"\s+", ".").ToLower()}@falufri.se";
        }

        public override string ToString()
        {
            return $"{Name} | {TeacherEmail}";
        }
    }
}
