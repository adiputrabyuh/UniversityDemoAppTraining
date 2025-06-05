using MySql.Data.MySqlClient;
using System.Data;
using UniversityDemoAppTraining.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UniversityDemoAppTraining.Services
{
    public class EnrollmentService : DbMySQLService
    {
        public List<Enrollment> GetEnrollmentsAll()
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string enrollmentSql = "SELECT enrollment_id, student_id, course_id, teacher_id, grade From enrollment;";
                MySqlCommand command = new MySqlCommand(enrollmentSql, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Enrollment enrollment = new Enrollment()
                        {
                            enrollment_id = reader.GetInt32("enrollment_id"),
                            student_id = reader.GetInt32("student_id"),
                            course_id = reader.GetInt32("course_id"),
                            teacher_id = reader.GetInt32("teacher_id"),
                            grade = reader.GetString("grade")                         
                        };
                        enrollments.Add(enrollment);
                    }
                }
            }
            return enrollments;
        }



        public Enrollment GetByID(int enrollment_id)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string enrollmentSql = "SELECT enrollment_id, student_id, course_id, teacher_id, grade From enrollment WHERE enrollment_id = @enrollment_id;";
                using (MySqlCommand command = new MySqlCommand(enrollmentSql, connection))
                {
                    command.Parameters.AddWithValue("@enrollment_id", enrollment_id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Enrollment
                            {
                                enrollment_id = reader.GetInt32("enrollment_id"),
                                student_id = reader.GetInt32("student_id"),
                                course_id = reader.GetInt32("course_id"),
                                teacher_id = reader.GetInt32("teacher_id"),
                                grade = reader.GetString("grade")
                            };
                        }
                    }
                }
            }
            return new Enrollment();
        }


        public bool AddEnrollment(Enrollment enrollment)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string checkStudent = "SELECT COUNT(*) FROM student WHERE student_id = @student_id";
                MySqlCommand stdCmd = new MySqlCommand(checkStudent, connection);
                stdCmd.Parameters.AddWithValue(@"student_id", enrollment.student_id);
                int stdCount = Convert.ToInt32(stdCmd.ExecuteScalar());

                string checkCourse = "SELECT COUNT(*) FROM course WHERE course_id = @course_id";
                MySqlCommand crsCmd = new MySqlCommand(checkCourse, connection);
                crsCmd.Parameters.AddWithValue(@"course_id", enrollment.course_id);
                int crsCount = Convert.ToInt32(crsCmd.ExecuteScalar());

                string checkTeacher = "SELECT COUNT(*) FROM teacher WHERE teacher_id = @teacher_id";
                MySqlCommand tcrCmd = new MySqlCommand(checkTeacher, connection);
                tcrCmd.Parameters.AddWithValue(@"teacher_id", enrollment.teacher_id);
                int tcrCount = Convert.ToInt32(tcrCmd.ExecuteScalar());

                if (crsCount == 0 && stdCount == 0 && tcrCount == 0)
                {
                    return false;
                }

                string EnrollmentSql = "INSERT INTO enrollment(student_id, course_id, teacher_id, grade) VALUES(@student_id, @course_id, @teacher_id, @grade);";
                using (MySqlCommand command = new MySqlCommand(EnrollmentSql, connection))
                {
                    command.Parameters.AddWithValue(@"student_id", enrollment.student_id);
                    command.Parameters.AddWithValue(@"course_id", enrollment.course_id);
                    command.Parameters.AddWithValue(@"teacher_id", enrollment.teacher_id);
                    command.Parameters.AddWithValue(@"grade", enrollment.grade);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }



        public bool UpdateEnrollment(int enrollment_id, int student_id, int course_id, int teacher_id, string grade)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string checkStudent = "SELECT COUNT(*) FROM student WHERE student_id = @student_id";
                MySqlCommand stdCmd = new MySqlCommand(checkStudent, connection);
                stdCmd.Parameters.AddWithValue(@"student_id", student_id);
                int stdCount = Convert.ToInt32(stdCmd.ExecuteScalar());

                string checkCourse = "SELECT COUNT(*) FROM course WHERE course_id = @course_id";
                MySqlCommand crsCmd = new MySqlCommand(checkCourse, connection);
                crsCmd.Parameters.AddWithValue(@"course_id", course_id);
                int crsCount = Convert.ToInt32(crsCmd.ExecuteScalar());

                string checkTeacher = "SELECT COUNT(*) FROM teacher WHERE teacher_id = @teacher_id";
                MySqlCommand tcrCmd = new MySqlCommand(checkTeacher, connection);
                tcrCmd.Parameters.AddWithValue(@"teacher_id", teacher_id);
                int tcrCount = Convert.ToInt32(tcrCmd.ExecuteScalar());

                if (crsCount == 0 && stdCount == 0 && tcrCount == 0)
                {
                    return false;
                }

                string EnrollmentSql = "UPDATE enrollment SET enrollment_id = @enrollment_id, student_id = @student_id, course_id = @course_id, teacher_id = @teacher_id, grade = @grade WHERE enrollment_id = @enrollment_id;";
                using (MySqlCommand command = new MySqlCommand(EnrollmentSql, connection))
                {
                    command.Parameters.AddWithValue("@enrollment_id", enrollment_id);
                    command.Parameters.AddWithValue(@"student_id", student_id);
                    command.Parameters.AddWithValue(@"course_id", course_id);
                    command.Parameters.AddWithValue(@"teacher_id", teacher_id);
                    command.Parameters.AddWithValue(@"grade", grade);
                   

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeleteEnrollment(int enrollment_id)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string enrollmentSql = "DELETE FROM enrollment WHERE enrollment_id = @enrollment_id;";
                using (MySqlCommand command = new MySqlCommand(enrollmentSql, connection))
                {
                    command.Parameters.AddWithValue("@enrollment_id", enrollment_id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
