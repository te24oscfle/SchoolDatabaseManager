using SchoolDatabaseManager.Classes.Models;
using SchoolDatabaseManager.Helpers;
using Group = SchoolDatabaseManager.Classes.Models.Group;

namespace SchoolDatabaseManager.Database
{
    public static class DatabaseManager
    {
        // =========================================================
        // === STUDENTS
        // =========================================================
        public static void AddStudent(Student student)
        {
            DatabaseHelper.Write
            (
                """
                INSERT INTO students
                VALUES (name, student_email, group_id
                """,
                command => DatabaseHelper.WriteStudent(command, student)
            );
            Console.WriteLine($"Added Student {student.Name} to the database");
        }

        public static void RemoveStudent(int studentId)
        {
            DatabaseHelper.Write
            (
                """
                DELETE FROM students
                WHERE student_id = @student_id
                """,
                command => command.Parameters.AddWithValue("student_id", studentId)
            );
            Console.WriteLine($"Removed Student with ID={studentId} from the database.");
        }

        public static void AssignStudentToGroup(int studentId, int groupId)
        {
            DatabaseHelper.Write
            (
                """
                UPDATE students
                SET group_id = @group_id
                VALUES (name, student_email, group_id
                """,
                command => DatabaseHelper.WriteStudent(command, student)
            );

            Console.WriteLine($"Assigned Student with ID={studentId} to Group with ID={groupId}.");
        }

        public static List<Student> GetStudents()
        {
            // Get command
            using var connection = GetConnection();

            using NpgsqlCommand command = new NpgsqlCommand(
                """
                SELECT * FROM students
                ORDER BY id ASC
                """,
                connection
            );

            // Get reader object
            using NpgsqlDataReader reader = command.ExecuteReader();

            // Read all rows and create students
            List<Student> students = new List<Student>();
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                string studentEmail = reader.GetString(reader.GetOrdinal("student_email"));
                int groupId = reader.IsDBNull(reader.GetOrdinal("group_id")) ? -1 : reader.GetInt32(reader.GetOrdinal("group_id"));

                Student student = new Student(id, name, studentEmail, groupId);
                students.Add(student);
            }

            return students;
        }

        public static List<Student> GetStudentsInGroup(int groupId)
        {
            // Get command
            using var connection = GetConnection();

            using var command = new NpgsqlCommand(
                """
                    SELECT * FROM students
                    WHERE group_id = @group_id
                    ORDER BY id ASC
                """,
                connection
            );

            command.Parameters.AddWithValue("group_id", groupId);

            // Get reader object
            using NpgsqlDataReader reader = command.ExecuteReader();

            // Read all rows and create students
            List<Student> students = new List<Student>();
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                string studentEmail = reader.GetString(reader.GetOrdinal("student_email"));
                int studentGroupId = reader.GetInt32(reader.GetOrdinal("group_id"));

                Student student = new Student(id, name, studentEmail, studentGroupId);
                students.Add(student);
            }

            return students;
        }

        // =========================================================
        // === TEACHERS
        // =========================================================

        public static void AddTeacher(Teacher teacher)
        {
            // Open a connection
            using var connection = GetConnection();

            using NpgsqlCommand command = new NpgsqlCommand(
                """
                INSERT INTO teachers (name, teacher_email)
                VALUES (@name, @teacher_email)
                RETURNING id
                """,
                connection
            );

            // Insert values into command
            command.Parameters.AddWithValue("name", teacher.Name);
            command.Parameters.AddWithValue("teacher_email", teacher.TeacherEmail);

            // Execute the command and get the ID. If no valid ID exists, the student is giving a temp -1 id.
            teacher.Id = command.ExecuteScalar() is int id ? id : -1;

            Console.WriteLine($"Added Teacher {teacher.Name} to the database. (ID={teacher.Id})");
        }

        public static void RemoveTeacher(int teacherId)
        {
            // Open a connection
            using var connection = GetConnection();

            using NpgsqlCommand command = new NpgsqlCommand(
                """
                DELETE FROM teachers
                WHERE id = @teacher_id;
                """,
                connection
            );

            command.Parameters.AddWithValue("teacher_id", teacherId);

            command.ExecuteNonQuery();
            Console.WriteLine($"Removed Teacher with ID={teacherId} from the database.");
        }

        public static void AssignTeacherToGroup(int teacherId, int groupId)
        {
            using var connection = GetConnection();
            using var command = new NpgsqlCommand(
                """
                UPDATE teachers
                SET group_id = @group_id
                WHERE id = @teacher_id
                """,
                connection
            );

            command.Parameters.AddWithValue("group_id", groupId);
            command.Parameters.AddWithValue("teacher_id", teacherId);

            command.ExecuteNonQuery();
            Console.WriteLine($"Assigned Teacher with ID={teacherId} to Group with ID={groupId}.");
        }

        public static List<Teacher> GetTeachers()
        {
            // Get command
            using var connection = GetConnection();

            using NpgsqlCommand command = new NpgsqlCommand(
                """
                SELECT * FROM teachers
                ORDER BY id ASC
                """,
                connection
            );

            // Get reader object
            using NpgsqlDataReader reader = command.ExecuteReader();

            // Read all rows and create students
            List<Teacher> teachers = new List<Teacher>();
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                string teacherEmail = reader.GetString(reader.GetOrdinal("teacher_email"));
                int groupId = reader.IsDBNull(reader.GetOrdinal("group_id")) ? -1 : reader.GetInt32(reader.GetOrdinal("group_id"));

                Teacher teacher = new Teacher(id, name, teacherEmail, groupId);
                teachers.Add(teacher);
            }

            return teachers;
        }

        public static List<Teacher> GetTeachersInGroup(int groupId)
        {
            // Get command
            using var connection = GetConnection();

            using var command = new NpgsqlCommand(
                """
                    SELECT * FROM teachers
                    WHERE group_id = @group_id
                    ORDER BY id ASC
                """,
                connection
            );

            command.Parameters.AddWithValue("group_id", groupId);

            // Get reader object
            using NpgsqlDataReader reader = command.ExecuteReader();

            // Read all rows and create students
            List<Teacher> teachers = new List<Teacher>();
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                string teacherEmail = reader.GetString(reader.GetOrdinal("teacher_email"));
                int studentGroupId = reader.GetInt32(reader.GetOrdinal("group_id"));

                Teacher teacher = new Teacher(id, name, teacherEmail, studentGroupId);
                teachers.Add(teacher);
            }

            return teachers;
        }

        // =========================================================
        // === GROUPS
        // =========================================================

        public static void AddGroup(Group group)
        {
            // Get command
            using var connection = GetConnection();

            using var command = new NpgsqlCommand(
                """
                    INSERT INTO groups (name)
                    VALUES (@name)
                    RETURNING id
                """,
                connection
            );

            // Insert values into command
            command.Parameters.AddWithValue("name", group.Name);

            // Execute the command and get the ID. If no valid ID exists, the student is giving a temp -1 id.
            group.Id = command.ExecuteScalar() is int id ? id : -1;

            Console.WriteLine($"Added Group {group.Name} to the database. (ID={group.Id})");
        }

        public static List<Group> GetGroups()
        {
            using var connection = GetConnection();

            using NpgsqlCommand command = new NpgsqlCommand(
                """
                SELECT * FROM groups
                ORDER BY id ASC
                """, connection);

            // Get reader object
            using NpgsqlDataReader reader = command.ExecuteReader();

            // Read all rows and create students
            List<Group> groups = new List<Group>();
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("id"));
                string name = reader.GetString(reader.GetOrdinal("name"));

                Group group = new Group(name, id);
                groups.Add(group);
            }

            return groups;
        }

    }
}
