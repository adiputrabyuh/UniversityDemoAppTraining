using MySql.Data.MySqlClient;
using System.Data;
using UniversityDemoAppTraining.Models;
using Microsoft.AspNetCore.Http.HttpResults;
namespace UniversityDemoAppTraining.Services
{
    public class StudentService : DbMySQLService
    {

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
    }
}