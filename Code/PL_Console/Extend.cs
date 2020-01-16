using System;
using Persistence.MODEL;
using BL;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleTables;
using System.Collections.Generic;
using System.Globalization;



namespace PL_Console
{
    public class Extend
    {
        public static void InfoCustomer(string title, string[] menuItems, User us)
        {
            Console.Clear(); 
            Console.WriteLine("===========================================");
            Console.WriteLine(title);
            Console.WriteLine("===========================================");

            string[] infoUser = { us.UserAccount, us.UserName, us.UserEmail, us.UserPhoneNumber, us.UserBirthday.ToString(), us.UserGender };

            var table = new ConsoleTable("Tên Hiển Thị", us.UserAccount);
            table.AddRow("Tên Đăng Nhập", us.UserName);
            table.AddRow("Email", us.UserEmail);
            table.AddRow("Số Điện Thoại", us.UserPhoneNumber);
            table.AddRow("Ngày sinh", us.UserBirthday);
            table.AddRow("Giới Tính", us.UserGender);

            table.Write();
            Console.WriteLine("===========================================");
            Console.Write("Nhấn phím bất kì để quay lại ");
            Console.ReadKey();

        }

        public static short ShowListItems(string title, string[] menuItems, List<Item> items, int userID)
        {
            Console.Clear();
            short choice = -1;
            OrderBL orderBL = new OrderBL();

            var table = new ConsoleTable("ID", "Tên Đồ Uống", "Giá", "Size", "Nguyên Liệu"); // "itemNameEnglish",item.ItemNameEnglish,
            foreach (Item item in items)
            {
                table.AddRow(item.ItemID, item.ItemName, FormatCurrency(item.ItemPrice), item.ItemSize, item.ItemResources);
            }

            table.Write();
            if (items.Count <= 0)
            {
                Console.WriteLine("Không tìm thấy thông tin sản phẩm");
            }
            ItemBL itemBL = new ItemBL();

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + menuItems[i]);
            }

            Console.WriteLine("=========================================");
            try
            {
                Console.Write("#Lựa chọn: ");
                choice = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            if (choice < 0 || choice > menuItems.Length)
            {
                do
                {
                    try
                    {
                        Console.Write("#Please enter the correct selection ");
                        choice = Int16.Parse(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (choice < 0 || choice > menuItems.Length);
            }
            return choice;
        }

        public static short MenuDetails(string title, string[] menuItems)
        {

            short choose = 0;

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + menuItems[i]);
            }
            Console.WriteLine("=======================================");
            try
            {
                Console.Write("#Lựa chọn: ");
                choose = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            if (choose <= 0 || choose > menuItems.Length)
            {
                do
                {
                    try
                    {
                        Console.Write("Vui lòng nhập đúng lựa chọn: ");
                        choose = Int16.Parse(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (choose <= 0 || choose > menuItems.Length);
            }
            return choose;
        }
        public static short SelectItem(List<Item> items)  // selectItemByIDandSize
        {
            short ItemID = -1;
            // string ItemSize = "-1";
            bool isHave = false;
            try
            {
                Console.Write("Nhập ID sản phẩm: ");
                ItemID = Int16.Parse(Console.ReadLine());

                // Console.Write("Nhập Size sản phẩm: ");
                // short itemSize = Convert.ToInt16(ItemSize);
                // ItemSize = Console.ReadLine();
                
                
            }
            catch (System.Exception)
            {

            }
            foreach (var item in items)
            {
                if (ItemID == item.ItemID)
                {
                    isHave = true;
                }
            }
            if (!isHave)
            {
                do
                {
                    try
                    {
                        Console.Write("Vui lòng nhập lại: ");
                        ItemID = Int16.Parse(Console.ReadLine());
                        foreach (var item in items)
                        {
                            if (ItemID == item.ItemID)
                            {
                                isHave = true;
                            }
                        }
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (!isHave);

            }
            return ItemID;

        }
        public static string OnlyYN(string printcl)
        {
            string choice;
            Console.Write(printcl);
            choice = Console.ReadLine().ToUpper();
            while (true)
            {
                if (choice != "Y" && choice != "N")
                {
                    Console.Write("Bạn chỉ được nhập (Y/N): ");
                    choice = Console.ReadLine().ToUpper();
                    continue;
                }
                break;
            }

            return choice;
        }
        public static string FormatCurrency(double ItemPrice)
        {
            string a = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", ItemPrice);
            return a;
        }

    }
}
