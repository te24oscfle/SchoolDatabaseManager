using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SchoolDatabaseManager.Classes
{   
    public class Teacher
    {
        public int Id;
        public string Name;
        public string TeacherEmail;
        public int GroupId;

        public Teacher(string name)
        {
            Id = 0;
            Name = name;
            TeacherEmail = GetTeacherEmail();
            GroupId = 0;
        }

        public Teacher(int id, string name, string teacherEmail, int groupId)
        {
            Id = id;
            Name = name;
            TeacherEmail = teacherEmail;
            GroupId = groupId;
        }

        private string GetTeacherEmail()
        {
            string processedName = Name.ToLower()
                .Replace("å", "a")
                .Replace("ä", "a")
                .Replace("ö", "o"); // I should be doing something more robust here but this works
            string teacherEmail = $"{Regex.Replace(processedName, @"\s+", ".").ToLower()}@falufri.se";
            return teacherEmail;
        }
        
        public void GenerateTeacherEmail()
        {
            TeacherEmail = GetTeacherEmail();
        }

        public override string ToString()
        {
            return $"{Name} | {TeacherEmail}";
        }
    }
}
