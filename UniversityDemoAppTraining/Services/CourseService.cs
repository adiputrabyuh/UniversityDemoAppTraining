using MySql.Data.MySqlClient;
using System.Data;
using UniversityDemoAppTraining.Models;
using Microsoft.AspNetCore.Http.HttpResults;
namespace UniversityDemoAppTraining.Services
{
    public class CourseService : DbMySQLService
    {

        // Get All the Courses
        public List<Course> GetCourseAll()
        {
            List<Course> courses = new List<Course>();
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string courseSql = "SELECT id, course_name, course_code FROM course;";
                MySqlCommand cmd = new MySqlCommand(courseSql, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Course course = new Course()
                        {
                            id = reader.GetInt32("id"),
                            course_name = reader.GetString("course_name"),
                            course_code = reader.GetString("course_code"),
                            //email = reader.GetString("email"),
                            //enrollment_date = reader.GetDateTime("enrollment_date")
                        };
                        courses.Add(course);
                    }
                }
            }
            return courses;

        }

        // Get the course by Id
        public Course? GetByID(int id)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string courseSql = "SELECT id, course_name, course_code FROM course WHERE id = @id;";
                MySqlCommand cmd = new MySqlCommand(courseSql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Course()
                        {
                            id = reader.GetInt32("id"),
                            course_name = reader.GetString("course_name"),
                            course_code = reader.GetString("course_code"),
                            //email = reader.GetString("email")
                            //enrollment_date = reader.GetDateTime("enrollment_date")
                        };
                    }
                }
            }
            return null;
        }

        //Add a new course
        public void AddCourse(Course course)
        {
            string courseSql = "INSERT INTO course (course_name, course_code) VALUES (@course_name, @course_code);";

            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                MySqlCommand courseCmd = new MySqlCommand(courseSql, connection);
                courseCmd.Parameters.AddWithValue("@course_name", course.course_name);
                courseCmd.Parameters.AddWithValue("@course_code", course.course_code);
                //courseCmd.Parameters.AddWithValue("@email", course.email);
               

                try
                {
                    // Execute the command to insert the course into the database
                    int rowsAffected = courseCmd.ExecuteNonQuery();

                    // Check if any rows were affected (this means the insert was successful)
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Course added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("No Course was added.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the query execution
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }
        }

        // Update a course
        public bool UpdateCourse(int id, string courseName, string courseCode)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string sql = "UPDATE course SET course_name = @course_name, course_code = @course_code WHERE id = @id;";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@course_name", courseName);
                    cmd.Parameters.AddWithValue("@course_code", courseCode);
                    //cmd.Parameters.AddWithValue("@email", email);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        //Delete a course
        public bool DeleteCourse(int id)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string courseSql = "DELETE FROM course WHERE id = @id;";
                using (MySqlCommand command = new MySqlCommand(courseSql, connection))
                {
                    command.Parameters.AddWithValue(@"id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}