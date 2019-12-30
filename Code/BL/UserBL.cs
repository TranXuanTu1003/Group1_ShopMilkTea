using System;
using DAL;
using Persistence.MODEL;
using System.Text.RegularExpressions;

namespace BL
{
    public class UserBL
    {
        private UserDAL userDAL;
        public UserBL()
        {
            userDAL = new UserDAL();
        }
        public User GetUserByUserNameAndPassword(string username, string password)
        {
            if ((username == null) || (password == null))
            {
                return null;
            }
            Regex regex = new Regex(@"^[\b]");
            MatchCollection matchCollectionUserName = regex.Matches(username);
            MatchCollection matchCollectionPassWord = regex.Matches(password);
            if (matchCollectionUserName.Count < username.Length || matchCollectionPassWord.Count < password.Length)
            {
                return null;
            }
            return userDAL.GetUserByUserNameAndPassword(username, password);
        }
        public User GetUserById(int? userId)
        {
            return userDAL.GetUserById(userId);
        }
    }
}
