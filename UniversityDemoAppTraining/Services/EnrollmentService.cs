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


        //public bool AddEnrollment(Enrollment enrollment)
        //{
        //    using (MySqlConnection connection = GetOpenMySqlConnection())
        //    {
        //        string checkStudent = "SELECT COUNT(*) FROM student WHERE StudentID = @StudentID";
        //        MySqlCommand stdCmd = new MySqlCommand(checkStudent, connection);
        //        stdCmd.Parameters.AddWithValue(@"StudentID", enrollment.StudentID);
        //        int stdCount = Convert.ToInt32(stdCmd.ExecuteScalar());

        //        string checkCourse = "SELECT COUNT(*) FROM course WHERE CourseID = @CourseID";
        //        MySqlCommand crsCmd = new MySqlCommand(checkCourse, connection);
        //        crsCmd.Parameters.AddWithValue(@"CourseID", enrollment.CourseID);
        //        int crsCount = Convert.ToInt32(crsCmd.ExecuteScalar());

        //        if (crsCount == 0 && stdCount == 0)
        //        {
        //            return false;
        //        }
        //        string EnrollmentSql = "INSERT INTO enrollment(EnrollmentDate,EnrollmentStatus, GPA, StudentID, CourseID) VALUES(@EnrollmentDate,@EnrollmentStatus, @GPA, @StudentID, @CourseID);";
        //        using (MySqlCommand command = new MySqlCommand(EnrollmentSql, connection))
        //        {
        //            command.Parameters.AddWithValue(@"EnrollmentDate", enrollment.EnrollmentDate);
        //            command.Parameters.AddWithValue(@"EnrollmentStatus", enrollment.EnrollmentStatus);
        //            command.Parameters.AddWithValue(@"GPA", enrollment.GPA);
        //            command.Parameters.AddWithValue(@"StudentID", enrollment.StudentID);
        //            command.Parameters.AddWithValue(@"CourseID", enrollment.CourseID);

        //            int rowsAffected = command.ExecuteNonQuery();
        //            return rowsAffected > 0;
        //        }
        //    }
        //}



        //public bool UpdateEnrollment(int id, DateTime enrollmentDate, string enrollmentStatus, decimal gPA, int studentID, int courseID)
        //{
        //    using (MySqlConnection connection = GetOpenMySqlConnection())
        //    {
        //        string checkStudent = "SELECT COUNT(*) FROM student WHERE StudentID = @studentID";
        //        MySqlCommand stdCmd = new MySqlCommand(checkStudent, connection);
        //        stdCmd.Parameters.AddWithValue(@"StudentID", studentID);
        //        int stdCount = Convert.ToInt32(stdCmd.ExecuteScalar());

        //        string checkCourse = "SELECT COUNT(*) FROM course WHERE CourseID = @courseID";
        //        MySqlCommand crsCmd = new MySqlCommand(checkCourse, connection);
        //        crsCmd.Parameters.AddWithValue(@"CourseID", courseID);
        //        int crsCount = Convert.ToInt32(crsCmd.ExecuteScalar());

        //        if (crsCount == 0 && stdCount == 0)
        //        {
        //            return false;
        //        }
        //        string EnrollmentSql = "UPDATE enrollment SET EnrollmentDate = @enrollmentDate, EnrollmentStatus = @enrollmentStatus, GPA = @gPA, StudentID = @studentID, CourseID = @courseID WHERE EnrollmentID = @id;";
        //        using (MySqlCommand command = new MySqlCommand(EnrollmentSql, connection))
        //        {
        //            command.Parameters.AddWithValue("@id", id);
        //            command.Parameters.AddWithValue(@"EnrollmentDate", enrollmentDate);
        //            command.Parameters.AddWithValue(@"EnrollmentStatus", enrollmentStatus);
        //            command.Parameters.AddWithValue(@"GPA", gPA);
        //            command.Parameters.AddWithValue(@"StudentID", studentID);
        //            command.Parameters.AddWithValue(@"CourseID", courseID);

        //            int rowsAffected = command.ExecuteNonQuery();
        //            return rowsAffected > 0;
        //        }
        //    }
        //}

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
