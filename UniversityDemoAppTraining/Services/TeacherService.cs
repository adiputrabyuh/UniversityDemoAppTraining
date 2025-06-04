using MySql.Data.MySqlClient;
using System.Data;
using UniversityDemoAppTraining.Models;
using Microsoft.AspNetCore.Http.HttpResults;
namespace UniversityDemoAppTraining.Services
{
    public class TeacherService : DbMySQLService
    {

        // Get All the Teacher
        public List<Teacher> GetTeachersAll()
        {
            List<Teacher> teachers = new List<Teacher>();
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string teacherSql = "SELECT id, first_name, last_name, email FROM teacher;";
                MySqlCommand cmd = new MySqlCommand(teacherSql, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Teacher teacher = new Teacher()
                        {
                            id = reader.GetInt32("id"),
                            first_name = reader.GetString("first_name"),
                            last_name = reader.GetString("last_name"),
                            email = reader.GetString("email"),
                            //enrollment_date = reader.GetDateTime("enrollment_date")
                        };
                        teachers.Add(teacher);
                    }
                }
            }
            return teachers;

        }

        // Get the teacher by Id
        public Teacher? GetByID(int id)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string teacherSql = "SELECT id, first_name, last_name, email FROM teacher WHERE id = @id;";
                MySqlCommand cmd = new MySqlCommand(teacherSql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Teacher()
                        {
                            id = reader.GetInt32("id"),
                            first_name = reader.GetString("first_name"),
                            last_name = reader.GetString("last_name"),
                            email = reader.GetString("email")
                            //enrollment_date = reader.GetDateTime("enrollment_date")
                        };
                    }
                }
            }
            return null;
        }

        //Add a new teacher
        public void AddTeacher(Teacher teacher)
        {
            string teacherSql = "INSERT INTO teacher (first_name, last_name, email) VALUES (@first_name, @last_name, @email);";

            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                MySqlCommand teacherCmd = new MySqlCommand(teacherSql, connection);
                teacherCmd.Parameters.AddWithValue("@first_name", teacher.first_name);
               teacherCmd.Parameters.AddWithValue("@last_name", teacher.last_name);
                teacherCmd.Parameters.AddWithValue("@email", teacher.email);
                //studentCmd.Parameters.AddWithValue("@enrollment_date", student.enrollment_date);

                try
                {
                    // Execute the command to insert the teacher into the database
                    int rowsAffected = teacherCmd.ExecuteNonQuery();

                    // Check if any rows were affected (this means the insert was successful)
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Teacher added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("No teacher was added.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during the query execution
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }
        }

        // Update a teacher
        public bool UpdateTeacher(int id, string firstName, string lastName, string email)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string sql = "UPDATE teacher SET first_name = @first_name, last_name = @last_name, email = @email WHERE id = @id;";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@first_name", firstName);
                    cmd.Parameters.AddWithValue("@last_name", lastName);
                    cmd.Parameters.AddWithValue("@email", email);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        //Delete a teacher
        public bool DeleteTeacher(int id)
        {
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string teacherSql = "DELETE FROM teacher WHERE id = @id;";
                using (MySqlCommand command = new MySqlCommand(teacherSql, connection))
                {
                    command.Parameters.AddWithValue(@"id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}