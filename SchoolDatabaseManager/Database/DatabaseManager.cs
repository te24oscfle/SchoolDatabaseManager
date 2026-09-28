using SchoolDatabaseManager.Classes.Models;

namespace SchoolDatabaseManager.Database
{
    public static class DatabaseManager
    {
        #region Students
        public static void AddStudent(Student student)
        {
            bool success = DatabaseHelper.Write
            (
                """
                INSERT INTO students (name, student_email, group_id)
                VALUES (@name, @student_email, @group_id)
                """,
                command => DatabaseHelper.WriteStudent(command, student)
            );
            if (success)
                Console.WriteLine($"Added Student {student.Name} to the database.");
        }

        public static void RemoveStudent(int studentId)
        {
            bool success = DatabaseHelper.Write
            (
                """
                DELETE FROM students
                WHERE id = @id
                """,
                command => command.Parameters.AddWithValue("id", studentId)
            );
            if (success)
                Console.WriteLine($"Removed Student with ID={studentId} from the database.");
        }

        public static void AssignStudentToGroup(int studentId, int groupId)
        {
            bool success = DatabaseHelper.Write
            (
                """
                UPDATE students
                SET group_id = @group_id
                WHERE id = @id
                """,
                command => 
                {
                    command.Parameters.AddWithValue("group_id", groupId);
                    command.Parameters.AddWithValue("id", studentId);
                }
            );
            if (success)
                Console.WriteLine($"Assigned Student with ID={studentId} to Group with ID={groupId}.");
        }

        public static List<Student> GetStudents()
        {
            return DatabaseHelper.ReadToList(
                """
                SELECT * FROM students
                ORDER BY id ASC
                """,
                DatabaseHelper.ReadStudent
            );
        }

        public static List<Student> GetStudentsInGroup(int groupId)
        {
            return DatabaseHelper.ReadToList(
                """
                SELECT * FROM students
                WHERE group_id = @group_id
                ORDER BY id ASC
                """,
                DatabaseHelper.ReadStudent,
                command => command.Parameters.AddWithValue("group_id", groupId)
            );
        }

        #endregion

        #region Teachers

        public static void AddTeacher(Teacher teacher)
        {
            bool success = DatabaseHelper.Write
            (
                """
                INSERT INTO teachers (name, teacher_email, group_id)
                VALUES (@name, @teacher_email, @group_id)
                """,
                command => DatabaseHelper.WriteTeacher(command, teacher)
            );
            if (success)
                Console.WriteLine($"Added Teacher {teacher.Name} to the database.");
        }

        public static void RemoveTeacher(int teacherId)
        {
            bool success = DatabaseHelper.Write
            (
                """
                DELETE FROM teachers
                WHERE teacher_id = @teacher_id
                """,
                command => command.Parameters.AddWithValue("teacher_id", teacherId)
            );
            if (success)
                Console.WriteLine($"Removed Teacher with ID={teacherId} from the database.");
        }

        public static void AssignTeacherToGroup(int teacherId, int groupId)
        {
            bool success = DatabaseHelper.Write
            (
                """
                UPDATE teachers
                SET group_id = @group_id
                WHERE teacher_id = @teacher_id
                """,
                command =>
                {
                    command.Parameters.AddWithValue("group_id", groupId);
                    command.Parameters.AddWithValue("teacher_id", teacherId);
                }
            );
            if (success)
                Console.WriteLine($"Assigned Teacher with ID={teacherId} to Group with ID={groupId}.");
        }

        public static List<Teacher> GetTeachers()
        {
            return DatabaseHelper.ReadToList(
                """
                SELECT * FROM teachers
                ORDER BY id ASC
                """,
                DatabaseHelper.ReadTeacher
            );
        }

        public static List<Teacher> GetTeachersInGroup(int groupId)
        {
            return DatabaseHelper.ReadToList(
                """
                SELECT * FROM teachers
                WHERE group_id = @group_id
                ORDER BY id ASC
                """,
                DatabaseHelper.ReadTeacher,
                command => command.Parameters.AddWithValue("group_id", groupId)
            );
        }

        #endregion

        #region Groups

        public static void AddGroup(Group group)
        {
            bool success = DatabaseHelper.Write(
                """
                INSERT INTO groups (name)
                VALUES (@name)
                """,
                command => command.Parameters.AddWithValue("name", group.Name)
            );
            if (success)
                Console.WriteLine($"Added Group {group.Name} to the database.");
        }

        public static void RemoveGroup(int groupId)
        {
            bool success = DatabaseHelper.Write(
                """
                DELETE FROM groups
                WHERE group_id = @group_id
                """,
                command => command.Parameters.AddWithValue("group_id", groupId)
            );
            if (success)
                Console.WriteLine($"Removed Group with ID={groupId} from the database.");
        }

        public static List<Group> GetGroups()
        {
            return DatabaseHelper.ReadToList(
                """
                SELECT * FROM groups
                ORDER BY id ASC
                """,
                DatabaseHelper.ReadGroup
            );
        }

        #endregion
    }
}