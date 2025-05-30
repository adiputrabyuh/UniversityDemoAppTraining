using MySql.Data.MySqlClient;
using System.Data;
//using SimpleUniversityDemo.Models;
using SimpleUniversityDemo.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using MySqlX.XDevAPI;
using UniversityDemoAppTraining.Models;
using UniversityDemoAppTraining.Services;
namespace SimpleUniversityDemo.Services
{
    public class StudentService : DbMySQLService
    {

        public List<Student> GetStudentsAll()
        {
            List<Student> students = new List<Student>();
            using (MySqlConnection connection = GetOpenMySqlConnection())
            {
                string studentSql = "SELECT StudentID, FirstName, LastName, Address, Major FROM student;";
                MySqlCommand cmd = new MySqlCommand(studentSql, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student()
                        {
                            Id = reader.GetInt32("StudentID"),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            Email = reader.GetString("Address"),
                            EnrollDate = reader.GetDateTime("Major")
                        };
                        students.Add(student);
                    }
                }
            }
            return students;

        }
    }
}