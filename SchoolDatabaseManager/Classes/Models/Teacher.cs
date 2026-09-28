using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SchoolDatabaseManager.Classes.Models
{   
    public class Teacher
    {
        public int Id;
        public string Name;
        public string TeacherEmail;
        public int GroupId;

        public Teacher(string name)
        {
            Id = -1;
            Name = name;
            GroupId = -1;

            GenerateTeacherEmail();
        }

        public Teacher(int id, string name, string teacherEmail, int groupId)
        {
            Id = id;
            Name = name;
            TeacherEmail = teacherEmail;
            GroupId = groupId;
        }

        public string GenerateTeacherEmail()
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
