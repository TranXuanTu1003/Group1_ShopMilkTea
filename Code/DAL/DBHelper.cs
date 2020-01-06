using System;
using System.IO;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DBHelper
    {

        private static MySqlConnection connection = null;

        public static MySqlConnection OpenConnection()
        {
            try
            {
                string connectionString;

                FileStream fileStream = File.OpenRead("ConnectionString.txt");
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    connectionString = reader.ReadLine();
                }
                fileStream.Close();

                return OpenConnection(connectionString);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static MySqlConnection OpenConnection(string connection_String)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = connection_String
                };
                connection.Open();
                return connection;
            }
            catch
            {
                return null;
            }
        }

        public static void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        public static MySqlDataReader ExecQuery(string query, MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            return command.ExecuteReader();
        }

        public static int ExecNonQuery(string query, MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;

            return command.ExecuteNonQuery();
        }
    }
}