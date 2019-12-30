using System;
using System.IO;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DbHelper
    {
        
        private static MySqlConnection connection;

        public static MySqlConnection OpenConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = @"server=localhost;
                                    userid=root;
                                    password=cayiula1103;
                                    port=3306;
                                    database=QuanLyBanTraSua;"
                };
            }
            return connection;
        }

        public static MySqlConnection OpenConnection(string connectionString)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = connectionString
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
                connection.Clone();
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