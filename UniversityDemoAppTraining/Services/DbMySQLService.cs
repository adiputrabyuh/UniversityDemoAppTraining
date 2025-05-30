using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Data;
using MySql.Data.MySqlClient;


namespace UniversityDemoAppTraining.Services
// Environtment Variable (To connect database more safety)
{
    public class DbMySQLService

    {
        private readonly string _connectionString;

        public DbMySQLService() {
                string[] user = Environment.GetEnvironmentVariable("DB_INFORMATION").Split(':');
                _connectionString = "server=localhost; userid=" + user[0] + "; password" + user[1] + "; database=universitydemoapp";
            }

        public MySqlConnection GetOpenMySqlConnection()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public static bool ExecuteUpdate(MySqlTransaction act, MySqlCommand cmd)
        {
            int rc = cmd.ExecuteNonQuery();
            if (rc != 0)
            {
                act.Commit();
                return true;
            }
            act.Rollback();
            return false;
        }
        public bool TestConnection()
        {
            try
            {
                using (var connection = GetOpenMySqlConnection())
                {
                    return connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Database connection failed:" + ex.Message);
                return false;
            }
        }

    }
}
