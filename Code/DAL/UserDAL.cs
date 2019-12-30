using System;
using MySql.Data.MySqlClient;
using Persistence.MODEL;
using System.Text.RegularExpressions;

namespace DAL
{
    public class UserDAL
    {
        private MySqlDataReader reader;
        private string query;

        public UserDAL(){}
        public User GetUserByUserNameAndPassword(string username, string password){
            if((username == null) || (password == null)){
                return null;
            }
            Regex regex = new Regex(@"^[\b]");
            MatchCollection matchCollectionUserName = regex.Matches(username);
            MatchCollection matchCollectionPassword = regex.Matches(password);
            if(matchCollectionUserName.Count < username.Length || matchCollectionPassword.Count < password.Length){
                return null;
            }
            query = $@"select * from Users where userName = '{username}' and userPassWord = '{password}'";
            try
            {
                reader = DbHelper.ExecQuery(query,DbHelper.OpenConnection());
            }
            catch (System.Exception)
            {
                Console.WriteLine("Không thể kết nối tới cơ sở dữ liệu");
                return null;
            }
            User user = null;
            if(reader.Read()){
                user = GetUser(reader);
            }
            reader.Close();
            DbHelper.CloseConnection();
            return user;
        }

        public User GetUserById(int? userId){
            if(userId == null){
                return null;
            }
            query = $@"select * from Users where userID = {userId};";
            reader = DbHelper.ExecQuery(query,DbHelper.OpenConnection());
            User user = null;

            if(reader.Read()){
                user = GetUser(reader);
            }
            reader.Close();
            DbHelper.CloseConnection();
            return user;
        }
        private User GetUser(MySqlDataReader reader){
            User user = new User();
            user.UserID = reader.GetInt32("userID");
            user.UserName = reader.GetString("userName");
            user.PassWord = reader.GetString("userPassWord");
            user.UserEmail = reader.GetString("userEmail");
            user.UserPhoneNumber = reader.GetString("userPhoneNumber");
            user.UserBirthday = reader.GetDateTime("userBirthday");
            user.UserGender = reader.GetString("userGender");
            return user;
        }
    }

    
}
