using System;
using DAL;
using Persistence.MODEL;
using System.Text.RegularExpressions;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

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
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionUserName = regex.Matches(username);
            MatchCollection matchCollectionPassWord = regex.Matches(password);
            if (matchCollectionUserName.Count < username.Length || matchCollectionPassWord.Count < password.Length)
            {
                return null;
            }
            return userDAL.GetUserByUserNameAndPassword(username, password);
        }
        public User GetUserById(int? userID)
        {
            return userDAL.GetUserById(userID);
        }

        public bool UpdateStatusShoppingCartById(bool isHave, int? userID)
        {
            return userDAL.UpdateStatusShoppingCartById(isHave, userID);
        }

        public int ConfirmRegistration(string userName, string userEmail){
            return userDAL.ConfirmRegister(userName, userEmail);
        }

        public int Register(string userName, string userPassword, string userDisplayName, string userEmail, string userPhoneNumber, string userBirthday, string userGender)
        {
           return userDAL.Register(userName, userPassword,  userDisplayName, userEmail, userPhoneNumber,  userBirthday, userGender);
        }

        public static string Password()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        sb.Length--;
                    }
                    continue;
                }
                Console.Write('*');

                sb.Append(cki.KeyChar);
            }
            return sb.ToString();
        }
    }
}
