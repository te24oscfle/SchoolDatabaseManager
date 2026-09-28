using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDatabaseManager.Classes
{
    public class Group
    {
        public int Id;
        public string Name;
        private List<Student> Students;
        private List<Teacher> Teachers;

        public Group(string name, int id)
        {
            Name = name;
            Id = id;

            Students = new List<Student>();
            Teachers = new List<Teacher>();
        }

        public Group(string name) : this(name, 0) { }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }

        public void RemoveStudent(string studentName)
        {
            Student? student = Students.FirstOrDefault(student => student.Name == studentName);

            if (student == null)
                throw new ArgumentException($"Could not find Student named {studentName} in Group {Name}");

            RemoveStudent(student);
        }

        public void AddTeacher(Teacher teacher)
        {
            Teachers.Add(teacher);
        }

        public void RemoveTeacher(Teacher teacher)
        {
            Teachers.Remove(teacher);
        }

        public void RemoveTeacher(string teacherName)
        {
            Teacher? teacher = Teachers.FirstOrDefault(teacher => teacher.Name == teacherName);

            if (teacher == null)
                throw new ArgumentException($"Could not find Teacher named {teacherName} in Group {Name}");

            RemoveTeacher(teacher);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
