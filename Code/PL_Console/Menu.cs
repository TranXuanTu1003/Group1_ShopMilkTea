using System;
using BL;
using Persistence.MODEL;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using ConsoleTables;
using System.Collections.Generic;

namespace PL_Console
{
    public class Menu
    {

        public void Program()
        {
            int choose;
            Console.Clear();
            Console.WriteLine("\n-----Welcome To The Happy MilkTea Shop-----");
            Console.WriteLine("===========================================");
            Console.WriteLine("\n1. Đăng nhập");
            Console.WriteLine("2. Đăng ký");
            Console.WriteLine("0. Thoát\n");
            Console.WriteLine("-------------------------------------------");
            Console.Write("\nNhập lựa chọn: ");
            while (true)
            {
                bool check = Int32.TryParse(Console.ReadLine(), out choose);
                if (check == false)
                {
                    Console.WriteLine("Bạn nhập sai, Hãy nhập lại: ");
                    Console.Write("Nhập lựa chọn: ");
                }
                else if (choose < 0 || choose > 2)
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
                    MenuLogin();
                    break;
                case 2:
                    MenuRegistration();
                    break;
                case 0:
                    {
                        Console.Write("Thoát thật hay đùa mày? (Y/N): ");
                        string choice;
                        while (true)
                        {
                            choice = Console.ReadLine();
                            if (choice == "Y" || choice == "y" || choice == "N" || choice == "n")
                            {
                                if (choice == "Y" || choice == "y")
                                {
                                    Environment.Exit(0);
                                    break;
                                }
                                else
                                {
                                    Program();
                                    continue;
                                }
                            }
                            else
                            {
                                Console.Write("Bạn chỉ được nhập (Y/N): ");
                            }
                        }
                        break;
                    }
            }
        }


        public void MenuLogin()
        {
            Console.Clear();
            UserBL userBL = new UserBL();
            string username = null;
            string password = null;
            while (true)
            {
                User user = new User();
                Console.WriteLine("===========================================");
                Console.WriteLine("Đăng nhập");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Nhập Tên Tài Khoản: ");
                username = Console.ReadLine();
                Console.Write("Nhập Mật Khẩu: ");
                password = UserBL.Password();


                if ((ValiDateLogin(username) == false) || (ValiDateLogin(password) == false))
                {
                    Console.WriteLine("Tài khoản hoặc Mật khẩu không chính xác!");
                    Console.Write("Bạn có muốn tiếp tục ? (Y/N): ");
                    string choice;
                    while (true)
                    {
                        choice = Console.ReadLine();
                        if (choice == "Y" || choice == "y" || choice == "N" || choice == "n")
                        {
                            if (choice == "Y" || choice == "y")
                            {
                                MenuLogin();
                                continue;
                            }
                            else
                            {
                                Program();
                                break;
                            }
                        }
                        else
                        {
                            Console.Write("Bạn chỉ được nhập (Y/N): ");
                        }
                    }
                }

                user = userBL.GetUserByUserNameAndPassword(username, password);

                if (user == null)
                {
                    Console.WriteLine("Tài khoản hoặc Mật khẩu không chính xác");
                    Console.Write("Bạn có muốn tiếp tục ? (Y/N): ");
                    string choice;
                    while (true)
                    {
                        choice = Console.ReadLine();
                        if (choice == "Y" || choice == "y" || choice == "N" || choice == "n")
                        {
                            if (choice == "Y" || choice == "y")
                            {
                                MenuLogin();
                                continue;
                            }
                            else
                            {
                                Program();
                                break;
                            }
                        }
                        else
                        {
                            Console.Write("Bạn chỉ được nhập (Y/N): ");
                        }
                    }

                }
                else
                {
                    ShowProduct show = new ShowProduct();
                    show.MenuProduct(user);
                    break;
                }

            }
        }

        public void MenuRegistration()
        {
            Console.Clear();
            User user = new User();
            UserBL userBL = new UserBL();
            string username = null;
            string password = null;
            string displayname = null;
            string email = null;
            string phoneNumber = null;
            string birthday =null;
            string gender = null;

            Console.WriteLine("===========================================");
            Console.WriteLine("----------Tạo Tài Khoản -------------------");
            Console.WriteLine("-------------------------------------------");
            Console.Write("Enter UserName: ");
            while (true)
            {
                username = Console.ReadLine();
                Regex regex = new Regex("[a-zA-Z0-9_]");

                if (regex.IsMatch(username))
                {
                    user.UserName = username;
                    break;
                }
                else
                {
                    Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
                    Console.Write("Enter Username: ");
                }

            }
            Console.Write("Enter Password: ");
            while (true)
            {
                // password = Console.ReadLine();
                password = UserBL.Password();
                Regex regex = new Regex("[a-zA-Z0-9_]");

                if (regex.IsMatch(password))
                {
                    user.PassWord = password;
                    break;
                }
                else
                {
                    Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
                    Console.Write("Enter Password: ");
                }

            }
            Console.Write("Enter DisplayName: ");
            while (true)
            {
                displayname = Console.ReadLine();
                Regex regex = new Regex("[a-zA-Z0-9_]");

                if (regex.IsMatch(displayname))
                {
                    user.UserAccount = displayname;
                    break;
                }
                else
                {
                    Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
                    Console.Write("Enter DisplayName: ");
                }

            }
            Console.Write("Enter Email: ");
            while (true)
            {
                email = Console.ReadLine();
                Regex regex = new Regex("[a-zA-Z0-9_]");

                if (regex.IsMatch(email))
                {
                    user.UserEmail = email;
                    break;
                }
                else
                {
                    Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
                    Console.Write("Enter Email: ");
                }

            }
            Console.Write("Enter PhoneNumber: ");
            while (true)
            {
                phoneNumber = Console.ReadLine();
                Regex regex = new Regex("[0-9]");

                if (regex.IsMatch(phoneNumber))
                {
                    user.UserPhoneNumber = phoneNumber;
                    break;
                }
                else
                {
                    Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
                    Console.Write("Enter PhoneNumber: ");
                }

            }
            Console.Write("Enter BirthDay: ");
            while (true)
            {
                birthday = Console.ReadLine();
                Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((1[9]|2[0])\d\d))$");

                if (regex.IsMatch(birthday))
                {
                    user.UserBirthday = birthday;
                    break;
                }
                else
                {
                    Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
                    Console.Write("Enter BirthDay: ");
                }

            }
            Console.Write("Enter Gender: ");
            while (true)
            {
                gender = Console.ReadLine();
                Regex regex = new Regex(@"^M(ale)?$|^F(emale)?$|^m(ale)?$|^f(emale)?$");
                if (regex.IsMatch(gender))
                {
                    user.UserGender = gender;
                    break;
                }
                else
                {
                    Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
                    Console.Write("Enter Gender: ");
                }

            }


            int choice = userBL.ConfirmRegister(username, email);
            if (choice == 2)
            {
                userBL.Register(username, password, displayname, email, phoneNumber, birthday, gender);
                Console.WriteLine("Đăng kí thành công");
                Console.Write("Bạn có muốn tiếp tục ? (Y/N): ");
                string choice1;
                while (true)
                {
                    choice1 = Console.ReadLine();
                    if (choice1 == "Y" || choice1 == "y" || choice1 == "N" || choice1 == "n")
                    {
                        if (choice1 == "Y" || choice1 == "y")
                        {
                            Program();
                            break;
                        }
                        else
                        {
                            Program();
                            break;
                        }
                    }
                    else
                    {
                        Console.Write("Bạn chỉ được nhập (Y/N): ");
                    }
                }
            }
            else
            {
                Console.WriteLine("Tên đăng nhập hoặc Email đã tồn tại!");
                Console.Write("Bạn có muốn tiếp tục ? (Y/N): ");
                string choice2;
                while (true)
                {
                    choice2 = Console.ReadLine();
                    if (choice2 == "Y" || choice2 == "y" || choice2 == "N" || choice2 == "n")
                    {
                        if (choice2 == "Y" || choice2 == "y")
                        {
                            MenuRegistration();
                            continue;
                        }
                        else
                        {
                            Program();
                            break;
                        }
                    }
                    else
                    {
                        Console.Write("Bạn chỉ được nhập (Y/N): ");
                    }
                }
            }
        }

        public bool ValiDateLogin(string login)
        {
            Console.Clear();
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionLogin = regex.Matches(login);
            if (matchCollectionLogin.Count < login.Length)
            {
                return false;
            }
            else if (login == " ")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}



#region


// {
// Console.Write("Enter UserName: ");
// while (true)
// {
//     string username = Console.ReadLine();
//     Regex regex = new Regex("[a-zA-Z0-9_]");

//     if (regex.IsMatch(username))
//     {
//         user.UserName = username;
//         break;
//     }
//     else
//     {
//         Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
//         Console.Write("Enter Username: ");
//     }

// }
// Console.Write("Enter Password: ");
// while (true)
// {
//     string password = Console.ReadLine();
//     Regex regex = new Regex("[a-zA-Z0-9_]");

//     if (regex.IsMatch(password))
//     {
//         user.PassWord = password;
//         break;
//     }
//     else
//     {
//         Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
//         Console.Write("Enter Password: ");
//     }

// }
// Console.Write("Enter DisplayName: ");
// while (true)
// {
//     string displayname = Console.ReadLine();
//     Regex regex = new Regex("[a-zA-Z0-9_]");

//     if (regex.IsMatch(displayname))
//     {
//         user.UserAccount = displayname;
//         break;
//     }
//     else
//     {
//         Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
//         Console.Write("Enter DisplayName: ");
//     }

// }
// Console.Write("Enter Email: ");
// while (true)
// {
//     string email = Console.ReadLine();
//     Regex regex = new Regex("[a-zA-Z0-9_]");

//     if (regex.IsMatch(email))
//     {
//         user.UserEmail = email;
//         break;
//     }
//     else
//     {
//         Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
//         Console.Write("Enter Email: ");
//     }

// }
// Console.Write("Enter PhoneNumber: ");
// while (true)
// {
//     string phoneNumber = Console.ReadLine();
//     Regex regex = new Regex("[0-9]");

//     if (regex.IsMatch(phoneNumber))
//     {
//         user.UserPhoneNumber = phoneNumber;
//         break;
//     }
//     else
//     {
//         Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
//         Console.Write("Enter PhoneNumber: ");
//     }

// }
// Console.Write("Enter BirthDay: ");
// while (true)
// {
//     string birthday = Console.ReadLine();
//     Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((1[9]|2[0])\d\d))$");

//     if (regex.IsMatch(birthday))
//     {
//         user.UserBirthday = birthday;
//         break;
//     }
//     else
//     {
//         Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
//         Console.Write("Enter BirthDay: ");
//     }

// }
// Console.Write("Enter Gender: ");
// while (true)
// {
//     string gender = Console.ReadLine();
//     Regex regex = new Regex(@"^M(ale)?$|^F(emale)?$|^m(ale)?$|^f(emale)?$");
//     if (regex.IsMatch(gender))
//     {
//         user.UserGender = gender;
//         break;
//     }
//     else
//     {
//         Console.WriteLine("Bạn nhập sai, Hãy nhập lại!!!");
//         Console.Write("Enter Gender: ");
//     }

// }

// }

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

//     // public string Password()
//     // {
//     //     // Console.Write("Enter UserName:  ");
//     //     // string UserName = Console.ReadLine();

//     //     using (MD5 md5Hash = MD5.Create())
//     //     {
//     //         string hash = GetMd5Hash(md5Hash, source);

//     //         // Console.WriteLine("\nThe MD5 hash of password is: " + hash);

//     //         // Console.WriteLine("Verifying the hash...");
//     //         // Console.WriteLine("Login successful, Press enter to continue...");

//     //         if (VerifyMd5Hash(md5Hash, source, hash))
//     //         {
//     //             Console.WriteLine("The hashes are the same.");
//     //         }
//     //         else
//     //         {
//     //             Console.WriteLine("The hashes are not same.");
//     //         }
//     //     }

//     //  string InputPassword()
//     // {
//     //     string InputPass = "";
//     //     do
//     //     {
//     //         ConsoleKeyInfo key = Console.ReadKey(true);
//     //         if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
//     //         {
//     //             InputPass += key.KeyChar;
//     //             Console.Write("*");
//     //         }
//     //         else
//     //         {
//     //             if (key.Key == ConsoleKey.Backspace && InputPass.Length > 0)
//     //             {
//     //                 InputPass = InputPass.Substring(0, (InputPass.Length - 1));
//     //                 Console.Write("\b \b");
//     //             }
//     //             else if (key.Key == ConsoleKey.Enter)
//     //             {
//     //                 break;
//     //             }
//     //         }
//     //     }
//     //     while (true);
//     //     return InputPass;
//     // }
//     //  string GetMd5Hash(MD5 md5Hash, string input)
//     // {

//     //     // Convert the input string to a byte array and compute the hash.
//     //     byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

//     //     // Create a new Stringbuilder to collect the bytes
//     //     // and create a string.
//     //     StringBuilder sBuilder = new StringBuilder();

//     //     // Loop through each byte of the hashed data 
//     //     // and format each one as a hexadecimal string.
//     //     for (int i = 0; i < data.Length; i++)
//     //     {
//     //         sBuilder.Append(data[i].ToString("x2"));
//     //     }

//     //     // Return the hexadecimal string.
//     //     return sBuilder.ToString();
//     // }

//     // // Verify a hash against a string.
//     //  bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
//     // {
//     //     // Hash the input.
//     //     string hashOfInput = GetMd5Hash(md5Hash, input);

//     //     // Create a StringComparer an compare the hashes.
//     //     StringComparer comparer = StringComparer.OrdinalIgnoreCase;

//     //     if (0 == comparer.Compare(hashOfInput, hash))
//     //     {
//     //         return true;
//     //     }
//     //     else
//     //     {
//     //         return false;
//     //     }
//     // }
// // }
// }
// }
#endregion
