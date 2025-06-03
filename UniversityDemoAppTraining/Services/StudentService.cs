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
                string studentSql = "SELECT id, first_name, last_name, email, enrollment_date FROM student;";
                MySqlCommand cmd = new MySqlCommand(studentSql, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student()
                        {
                            id = reader.GetInt32("id"),
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
        public Student? GetByID(int id)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string studentSql = "SELECT id, first_name, last_name, email, enrollment_date FROM student WHERE id = @id;";
                MySqlCommand cmd = new MySqlCommand(studentSql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student()
                        {
                            id = reader.GetInt32("id"),
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
    }
}