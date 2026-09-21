using Npgsql;
using SchoolDatabaseManager.Helpers;
using SchoolDatabaseManager.Classes;
using SchoolDatabaseManager.Classes.Models;

namespace SchoolDatabaseManager.Helpers
{
    public static class DatabaseManager
    {
        private static NpgsqlConnection GetConnection()
        {
            string connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            return connection;
        }

        // =========================================================
        // === STUDENTS
        // =========================================================
        public static void AddStudent(Student student)
        {
            // Open a connection
            using var connection = GetConnection();

            using NpgsqlCommand command = new NpgsqlCommand(
                """
                INSERT INTO students (name, student_email)
                VALUES (@name, @studentEmail)
                RETURNING id
                """,
                connection
            );
            
            // Insert values into command
            command.Parameters.AddWithValue("name", student.Name);
            command.Parameters.AddWithValue("studentEmail", student.StudentEmail);

            // Execute the command and get the ID. If no valid ID exists, the student is giving a temp -1 id.
            student.Id = command.ExecuteScalar() is int id ? id : -1;

            Console.WriteLine($"Added Student {student.Name} to the database. (ID={student.Id})");
        }

        public static void AssignStudentToGroup(int studentId, int groupId)
        {
            using var connection = GetConnection();

            using NpgsqlCommand command = new NpgsqlCommand(
                $"""
                UPDATE students
                SET group_id = @groupId
                WHERE id = @studentId
                """,
                connection
            );

            command.Parameters.AddWithValue("groupId", groupId);
            command.Parameters.AddWithValue("studentId", studentId);

            command.ExecuteNonQuery();

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
                int studentGroupId = reader.IsDBNull(reader.GetOrdinal("group_id")) ? -1 : reader.GetInt32(reader.GetOrdinal("group_id"));

                Student student = new Student(id, name, studentEmail, studentGroupId);
                students.Add(student);
            }

            return students;
        }
    }
}
