using MySql.Data.MySqlClient;
using System.Data;
using UniversityDemoAppTraining.Models;
using Microsoft.AspNetCore.Http.HttpResults;
namespace UniversityDemoAppTraining.Services
{
    public class StudentService : DbMySQLService
    {

        // Get All the Students
        public List<Student> GetStudentsAll()
        {
            List<Student> students = new List<Student>();
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string studentSql = "SELECT student_id, first_name, last_name, email, enrollment_date FROM student;";
                MySqlCommand cmd = new MySqlCommand(studentSql, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student()
                        {
                            student_id = reader.GetInt32("student_id"),
                            first_name = reader.GetString("first_name"),
                            last_name = reader.GetString("last_name"),
                            email = reader.GetString("email"),
                            enrollment_date = reader.GetDateTime("enrollment_date")
                        };
                        students.Add(student);
                    }
                }
            }
            return students;

        }

        // Get the student by Id
        public Student? GetByID(int student_id)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string studentSql = "SELECT student_id, first_name, last_name, email, enrollment_date FROM student WHERE student_id = @student_id;";
                MySqlCommand cmd = new MySqlCommand(studentSql, connection);
                cmd.Parameters.AddWithValue("@student_id", student_id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student()
                        {
                            student_id = reader.GetInt32("student_id"),
                            first_name = reader.GetString("first_name"),
                            last_name = reader.GetString("last_name"),
                            email = reader.GetString("email"),
                            enrollment_date = reader.GetDateTime("enrollment_date")
                        };
                    }
                }
            }
            return null;
        }

        //Add a new student
        public void AddStudent(Student student)
        {
            string studentSql = "INSERT INTO student (first_name, last_name, email) VALUES (@first_name, @last_name, @email);";

            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                MySqlCommand studentCmd = new MySqlCommand(studentSql, connection);
                studentCmd.Parameters.AddWithValue("@first_name", student.first_name);
                studentCmd.Parameters.AddWithValue("@last_name", student.last_name);
                studentCmd.Parameters.AddWithValue("@email", student.email);
                //studentCmd.Parameters.AddWithValue("@enrollment_date", student.enrollment_date);

                try
                {
                    // Execute the command to insert the student into the database
                    int rowsAffected = studentCmd.ExecuteNonQuery();

                    // Check if any rows were affected (this means the insert was successful)
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Student added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("No student was added.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the query execution
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }
        }

        // Update a student
        public bool UpdateStudent(int student_id, string firstName, string lastName, string email)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string sql = "UPDATE student SET first_name = @first_name, last_name = @last_name, email = @email WHERE student_id = @student_id;";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.Parameters.AddWithValue("@first_name", firstName);
                    cmd.Parameters.AddWithValue("@last_name", lastName);
                    cmd.Parameters.AddWithValue("@email", email);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        //Delete a student
        public bool DeleteStudent(int student_id)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string studentSql = "DELETE FROM student WHERE student_id = @student_id;";
                using (MySqlCommand command = new MySqlCommand(studentSql, connection))
                {
                    command.Parameters.AddWithValue(@"student_id", student_id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}