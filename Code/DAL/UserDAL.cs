using System;
using MySql.Data.MySqlClient;
using Persistence.MODEL;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DAL
{
    public class UserDAL
    {
        private MySqlDataReader reader;
        private string query;

        public UserDAL() { }
        public User GetUserByUserNameAndPassword(string username, string password)
        {
            if ((username == null) || (password == null))
            {
                return null;
            }
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionUserName = regex.Matches(username);
            MatchCollection matchCollectionPassword = regex.Matches(password);
            if (matchCollectionUserName.Count < username.Length || matchCollectionPassword.Count < password.Length)
            {
                return null;
            }
            query = $@"select * from Users where userName = '{username}' and userPassWord = '{password}'";
            try
            {
                reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            }
            catch (System.Exception)
            {
                Console.WriteLine("Không thể kết nối tới cơ sở dữ liệu");
                return null;
            }
            User user = null;
            if (reader.Read())
            {
                user = GetUser(reader);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return user;
        }

        public User GetUserById(int? userID)
        {
            if (userID == null)
            {
                return null;
            }
            query = $@"select * from Users where userID = {userID};";
            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            User user = null;

            if (reader.Read())
            {
                user = GetUser(reader);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return user;
        }

        public bool UpdateStatusShoppingCartById(bool isHave, int? userID)
        {

            if (userID == null)
            {
                return false;
            }
            switch (isHave)
            {
                case true:
                    query = $@"update Users set userShoppingCart = false where userID = {userID}";
                    break;
                case false:
                    query = $@"update Users set userShoppingCart = true where userID = {userID}";
                    break;
            }

            DBHelper.ExecNonQuery(query, DBHelper.OpenConnection());
            DBHelper.CloseConnection();
            return true;
        }
        private User GetUser(MySqlDataReader reader)
        {
            User user = new User();
            user.UserID = reader.GetInt32("userID");
            user.UserName = reader.GetString("userName");
            user.UserAccount = reader.GetString("userAccount");
            user.PassWord = reader.GetString("userPassWord");
            user.UserEmail = reader.GetString("userEmail");
            user.UserPhoneNumber = reader.GetString("userPhoneNumber");
            user.UserBirthday = reader.GetString("userBirthday");
            user.UserGender = reader.GetString("userGender");
            return user;
        }

        public int ConfirmRegister(string userName, string userEmail)
        {
            int a;
            query = $@"select * from Users where userName = '{userName}' and userEmail = '{userEmail}'";
            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                System.Console.WriteLine("Account or email  already exists");
                a = 1;
            }
            else
            {
                System.Console.WriteLine("Registration successful");
                a = 2;
            }
            reader.Close();
            DBHelper.CloseConnection();
            return a;
        }
        public int Register(string userName, string userPassword, string userDisplayName, string userEmail, string userPhoneNumber, string userBirthday, string userGender)
        {
            try
            {

                query = @"insert into Users(userName, userAccount, userPassWord, userEmail, userPhoneNumber, userBirthday, userGender)
                        values'(" + userName + "','" + userDisplayName + "','" + userPassword + "','" + userEmail + "','" + userPhoneNumber + "','" + userBirthday + "','" + userGender + "'');";
                MySqlConnection connection = DBHelper.OpenConnection();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("could not be registered!");
            }
            finally
            {
                DBHelper.CloseConnection();
            }
            return 100;
        }
    }


}
