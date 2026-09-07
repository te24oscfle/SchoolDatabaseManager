using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDatabaseManager.Classes.Models
{
    public class Group
    {
        public string name;
        private List<Student> students;
        private List<Teacher> teachers;

        public Group(string name, List<Student> students, List<Teacher> teachers)
        {
            this.name = name;
            this.students = students;
            this.teachers = teachers;
        }

        public Group(string name) : this(name, new List<Student>(), new List<Teacher>()) { }
        public Group(string name, List<Student> students) : this(name, students, new List<Teacher>()) { }
        public Group(string name, List<Teacher> teachers) : this(name, new List<Student>(), teachers) { }

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            students.Remove(student);
        }

        public void RemoveStudent(string name)
        {
            Student? student = students.FirstOrDefault(student => student.name == name);

            if (student == null)
                throw new ArgumentException($"Could not find Student named {name} in Group {this.name}");

            RemoveStudent(student);
        }

        public void AddTeacher(Teacher teacher)
        {
            teachers.Add(teacher);
        }

        public void RemoveTeacher(Teacher teacher)
        {
            teachers.Remove(teacher);
        }

        public void RemoveTeacher(string name)
        {
            Teacher? teacher = teachers.FirstOrDefault(teacher => teacher.name == name);

            if (teacher == null)
                throw new ArgumentException($"Could not find Teacher named {name} in Group {this.name}");

            RemoveTeacher(teacher);
        }
    }
}
