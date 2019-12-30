using System;
using BL;
using Persistence.MODEL;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace PL_Console
{
    class Menu
    {

        public void Program()
        {
            int choose;
            // Console.Clear();
            Console.WriteLine("-----Welcome To The Happy MilkTea Shop-----");
            Console.WriteLine("===========================================");
            Console.WriteLine("1. Login");
            Console.WriteLine("0. Exit");
            Console.WriteLine("===========================================");
            Console.WriteLine("Enter the selection: ");
            while (true)
            {
                bool check = Int32.TryParse(Console.ReadLine(), out choose);
                if (check == false)
                {
                    Console.WriteLine("Bạn nhập sai, Hãy nhập lại: ");
                    Console.Write("Nhập lựa chọn: ");
                }
                else if (choose < 0 || choose > 1)
                {
                    Console.WriteLine("Bạn nhập sai, Hãy nhập lại: ");
                    Console.Write("Nhập lựa chọn: ");
                }
                else
                {
                    break;
                }
            }
            switch (choose)
            {
                case 1:
                    // MenuLogin();
                    break;
                default:
                    break;
            }
        }
    }
}
# region
// public void MenuLogin()
// {
//     // Console.WriteLine("-------------");
//     UserBL userBL = new UserBL();
//     string username = null;
//     string password = null;
//     string choice;
//     while (true)
//     {
// User user = null;
// Console.WriteLine("===========================================");
// Console.WriteLine("Login ");
// Console.WriteLine("===========================================");
// Console.Write("UserName: ");
// username = Console.ReadLine();
// Console.Write("PassWord: ");
// password = Password();

// if (ValidateLogin(username) == false || ValidateLogin(password) == false)
// {

// Console.WriteLine("Username or password incorrect, please check again!!");
// Console.Write("Do you want to continue? (Y/N): ");
// while (true)
// {
//     choice = Console.ReadLine();
//     if (choice == "Y" || choice == "y" || choice == "N" || choice == "n")
//     {
//         if (choice == "Y" || choice == "y")
//         {
//             continue;
//         }
//         else if (choice == "N" || choice == "n")
//         {
//             Program();
//             break;
//         }
//     }
//     else
//     {
//         Console.Write("Bạn chỉ được nhập (Y/N): ");
//         choice = Console.ReadLine();
//         continue;
//     }

// }

//                     Console.WriteLine("Tên đăng nhập hoặc mật khẩu không được chứa kí tự đặc biệt");
//                     choice = Utility.OnlyYN("Bạn có muốn tiếp tục? Y/N: ");
//                     switch (choice)
//                     {
//                         case "Y":
//                             continue;
//                         case "N":
//                             Program();
//                             break;
//                     }

//                 }
//                 user = userBL.GetUserByUserNameAndPassword(username,password);
//                 if(username == null){
//                     Console.WriteLine("Tài khoản hoặc mật khẩu không đúng");
//                     choice = Utility.OnlyYN("Bạn có muốn tiếp tục? Y/N: ");
//                     switch (choice)
//                     {
//                         case "Y":
//                             continue;
//                         case "N":
//                             Program();
//                             break;
//                         default:
//                             continue;
//                     }
//                 }
//                 else
//                 {
//                     // Console.WriteLine("Logged in successfully!");
//                     ConsoleCus cc = new ConsoleCus();
//                     cc.MenuCus(user);
//                     break;
//                 }
//             }
//         }
//         public bool ValidateLogin(string login)
//         {
//             Regex regex = new Regex(@"^[\w+]");
//             MatchCollection matchCollectionlogin = regex.Matches(login);
//             if (matchCollectionlogin.Count < login.Length)
//             {
//                 return false;
//             }
//             else if (login == " ")
//             {
//                 return false;
//             }
//             else
//             {
//                 return true;
//             }
//         }
//         public string Password()
//         {
//             StringBuilder sb = new StringBuilder();
//             while (true)
//             {
//                 ConsoleKeyInfo cki = Console.ReadKey(true);
//                 if (cki.Key == ConsoleKey.Enter)
//                 {
//                     Console.WriteLine();
//                     break;
//                 }
//                 if (cki.Key == ConsoleKey.Backspace)
//                 {
//                     if (sb.Length > 0)
//                     {
//                         Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
//                         Console.Write(" ");
//                         Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
//                         sb.Length--;
//                     }
//                     continue;
//                 }
//                 Console.Write('*');

//                 sb.Append(cki.KeyChar);
//             }
//             return sb.ToString();
//         }

//         // public string Password()
//         // {
//         //     // Console.Write("Enter UserName:  ");
//         //     // string UserName = Console.ReadLine();

//         //     using (MD5 md5Hash = MD5.Create())
//         //     {
//         //         string hash = GetMd5Hash(md5Hash, source);

//         //         // Console.WriteLine("\nThe MD5 hash of password is: " + hash);

//         //         // Console.WriteLine("Verifying the hash...");
//         //         // Console.WriteLine("Login successful, Press enter to continue...");

//         //         if (VerifyMd5Hash(md5Hash, source, hash))
//         //         {
//         //             Console.WriteLine("The hashes are the same.");
//         //         }
//         //         else
//         //         {
//         //             Console.WriteLine("The hashes are not same.");
//         //         }
//         //     }

//         //  string InputPassword()
//         // {
//         //     string InputPass = "";
//         //     do
//         //     {
//         //         ConsoleKeyInfo key = Console.ReadKey(true);
//         //         if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
//         //         {
//         //             InputPass += key.KeyChar;
//         //             Console.Write("*");
//         //         }
//         //         else
//         //         {
//         //             if (key.Key == ConsoleKey.Backspace && InputPass.Length > 0)
//         //             {
//         //                 InputPass = InputPass.Substring(0, (InputPass.Length - 1));
//         //                 Console.Write("\b \b");
//         //             }
//         //             else if (key.Key == ConsoleKey.Enter)
//         //             {
//         //                 break;
//         //             }
//         //         }
//         //     }
//         //     while (true);
//         //     return InputPass;
//         // }
//         //  string GetMd5Hash(MD5 md5Hash, string input)
//         // {

//         //     // Convert the input string to a byte array and compute the hash.
//         //     byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

//         //     // Create a new Stringbuilder to collect the bytes
//         //     // and create a string.
//         //     StringBuilder sBuilder = new StringBuilder();

//         //     // Loop through each byte of the hashed data 
//         //     // and format each one as a hexadecimal string.
//         //     for (int i = 0; i < data.Length; i++)
//         //     {
//         //         sBuilder.Append(data[i].ToString("x2"));
//         //     }

//         //     // Return the hexadecimal string.
//         //     return sBuilder.ToString();
//         // }

//         // // Verify a hash against a string.
//         //  bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
//         // {
//         //     // Hash the input.
//         //     string hashOfInput = GetMd5Hash(md5Hash, input);

//         //     // Create a StringComparer an compare the hashes.
//         //     StringComparer comparer = StringComparer.OrdinalIgnoreCase;

//         //     if (0 == comparer.Compare(hashOfInput, hash))
//         //     {
//         //         return true;
//         //     }
//         //     else
//         //     {
//         //         return false;
//         //     }
//         // }
//     // }
//     }
// }
#endregion
