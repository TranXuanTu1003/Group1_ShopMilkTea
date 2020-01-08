using System;
using System.Collections.Generic;
using BL;
using System.Text;
using System.Text.RegularExpressions;
using Persistence.MODEL;
using ConsoleTables;
using DAL;
using System.Globalization;

namespace PL_Console
{
    public class ShowProduct
    {
        private ItemBL itemBL = new ItemBL();
        private UserBL userBL = new UserBL();
        private User user = new User();
        private Order order = new Order();

        public void MenuProduct(User us)
        {
            Console.Clear();
            user = us;
            while (true)
            {
                OrderBL orderBL = new OrderBL();
                int choice;
                Console.WriteLine("===========================================");
                Console.WriteLine("1. Xem thông tin tài khoản");
                Console.WriteLine("2. Xem danh sách sản phẩm");
                Console.WriteLine("3. Xem giỏ hàng");
                Console.WriteLine("4. Lịch sử mua hàng");
                Console.WriteLine("5. Đăng xuất");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Nhập lựa chọn: ");

                while (true)
                {
                    bool check = Int32.TryParse(Console.ReadLine(), out choice);
                    if (check == false)
                    {
                        Console.WriteLine("Bạn nhập sai, Hãy nhập lại: ");
                        Console.Write("Nhập lựa chọn: ");
                    }
                    else if (choice < 0 || choice > 5)
                    {
                        Console.WriteLine("Bạn nhập sai, Hãy nhập lại: ");
                        Console.Write("Nhập lựa chọn: ");
                    }
                    else
                    {
                        break;
                    }
                }
                switch (choice)
                {
                    case 1:
                        ShowInfoCustomer(us);
                        continue;
                    case 2:
                        ShowListItems();
                        continue;
                    case 3:
                        ShopingCart();
                        continue;
                    case 4:
                        ShowOrder();
                        continue;
                    case 5:
                        Console.Write("Đăng xuất thật đấy? (Y/N): ");
                        string chooce;
                        while (true)
                        {
                            chooce = Console.ReadLine();
                            if (chooce == "Y" || chooce == "y" || chooce == "N" || chooce == "n")
                            {
                                if (chooce == "Y" || chooce == "y")
                                {
                                    Environment.Exit(0);
                                    break;
                                }
                                else
                                {
                                    MenuProduct(us);
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
                break;

            }
        }

        public void ShowInfoCustomer(User us)
        {
            Console.Clear();
            Console.WriteLine("===========================================\n");
            Console.WriteLine("Thông tin tài khoản\n");
            Console.WriteLine("===========================================\n");
            string[] listinfo = { "Display Name", "User Name", "Email", "SDT", "Birth Date", "Gender" };

            Extend.InfoCustomer("Thông tin tài khoản", listinfo, us);

        }
        public void ShowListItems()
        {
            List<Item> items = null;

            items = itemBL.GetListItems();
            if (items == null)
            {
                Console.ReadKey();
            }
            else
            {
                while (true)
                {
                    int? ItemID;
                    string[] listcol = { "Chọn sản phẩm", "Tìm kiếm", "Hiển thị thêm sản phẩm", "Thoát" };
                    int choice = Extend.ShowListItems("Danh sách đồ uống", listcol, items, user.UserID);
                    switch (choice)
                    {
                        case 1:
                            if (items.Count <= 0)
                            {
                                Console.WriteLine("Chưa có sản phẩm");
                                Console.WriteLine("Nhấn phím bất kì để hiển thị danh sách đồ uống");
                                Console.ReadKey();
                                items = itemBL.GetListItems();
                            }
                            else
                            {
                                ItemID = Extend.SelectItem(items);
                                ShowAnItem(ItemID);
                            }
                            continue;
                        case 2:
                            Console.Write("Nhập tên sản phẩm: ");
                            Console.InputEncoding = Encoding.Unicode;
                            Console.OutputEncoding = Encoding.Unicode;
                            string itemName = Console.ReadLine();
                            items = itemBL.SearchItemName(itemName);
                            continue;
                        case 3:
                            Console.WriteLine("Chức năng đang phát triển");
                            Console.ReadKey();
                            continue;
                        case 4: 
                            break;

                    }
                    break;
                }
            }
        }

        public void ShowAnItem(int? ItemID)
        {
            while (true)
            {
                // Console.Clear();
                Item item = new Item();
                item = itemBL.GetItemByID(ItemID);

                var table = new ConsoleTable("Name", Convert.ToString(item.ItemName));
                table.AddRow("Name English: ", item.ItemNameEnglish);
                table.AddRow("Price: ", FormatCurrency(item.ItemPrice));
                table.AddRow("Size: ", item.ItemSize);
                // table.AddRow("Preview: ", item.ItemPreview);
                table.AddRow("Resources: ", item.ItemResources);
                // table.AddRow("Quantity: ", item.ItemQuantity);
                table.Write();
                Console.WriteLine();


                OrderBL orderBL = new OrderBL();
                string[] choice = { "Add To Cart", "Exit" };
                short choose = Extend.MenuDetails("Menu", choice);
                switch (choose)
                {
                    case 1:
                        AddToCart(item);
                        continue;

                    case 2:
                        
                        break;
                }
                break;
            }
        }

        public void AddToCart(Item item)
        {
            OrderBL orderBL = new OrderBL();
            order.OrderUser = new User();
            order.OrderItem = new Item();
            order.ListItems = new List<Item>();
            order.OrderUser.UserID = user.UserID;
            order.OrderItem.ItemID = item.ItemID;

            if (userBL.GetUserById(user.UserID).UserShoppingCart)
            {
                try
                {
                    if (orderBL.AddToShoppingCart(order))
                    {
                        Console.WriteLine("Sản phẩm đã được thêm vào giỏ hàng");
                    }
                    else
                    {
                        Console.WriteLine("Sản phẩm đã có trong giỏ hàng");
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            else
            {
                userBL.UpdateStatusShoppingCartById(false, user.UserID);
                order.CartStatus = 0;
                try
                {
                    if (orderBL.CreateShoppingCart(order))
                    {
                        Console.WriteLine("Sản phẩm đã được thêm vào giỏ hàng");
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }

            }
            Console.WriteLine("Nhấn phím bất kì để tiếp tục.");
            Console.ReadKey();
            
        }

        public void DeleteItems(Item item)
        {
            OrderBL orderBL = new OrderBL();
            if (orderBL.DeleteItemInShoppingCartByItemID(item.ItemID))
            {
                Console.WriteLine("Xoa thanh cong");
            }
            else
            {
                Console.WriteLine("sp nay chua co trong gio hang");
            }
            Console.WriteLine("nhan phim bat ki de tt");
            Console.ReadKey();
        }

        public void ShopingCart()
        {
            while (true)
            {
                // Console.Clear();
                OrderBL orderBL = new OrderBL();
                List<Item> shoppingCart = new List<Item>();
                shoppingCart = orderBL.ShowShoppingCartByUserID(user.UserID);
                if (shoppingCart == null)
                {
                    Console.ReadKey();
                    break;
                }
                double total = 0;
                if (shoppingCart.Count <= 0)
                {
                    Console.WriteLine("Chưa có sản phẩm");
                    Console.WriteLine("Nhấn phím bất kì để tiếp tục");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine($"Bạn có {shoppingCart.Count} loại sản phẩm trong giỏ hàng");
                    var table = new ConsoleTable("UserID", "UserName", "ItemSize", "ItemPrice");
                    foreach (var item in shoppingCart)
                    {
                        total = total + (double)item.ItemPrice;
                        table.AddRow(item.ItemID, item.ItemName, item.ItemSize, FormatCurrency(item.ItemPrice)); 

                    }
                    table.AddRow("", "", "", "");
                    table.AddRow("Total", "", "", FormatCurrency(total));
                    table.Write();
                    Console.WriteLine("Tổng tiền: {0}", FormatCurrency(total));

                    Console.WriteLine();
                    string[] choice = { "Thanh toán", "Xóa đồ uống khỏi giở hàng", "Thoát" };
                    short choose = Extend.MenuDetails("Menu", choice);
                    string pay;
                    switch (choose)
                    {
                        case 1:
                            pay = Extend.OnlyYN("Bạn có muốn thanh toán? (Y/N): ");
                            if (pay == "Y")
                            {
                                CreateOrder(total);
                            }
                            continue;
                        case 2:
                            Console.Write("Nhập mã đồ uống bạn muốn xóa: ");
                            int itemID;
                            bool b = Int32.TryParse(Console.ReadLine(), out itemID);
                            if (!b)
                            {
                                Console.WriteLine("Bạn chỉ được nhập số. Nhấn nút bất kì để quay lại");
                                Console.ReadKey();
                                continue;
                            }

                            bool y = false;
                            foreach (var item in shoppingCart)
                            {
                                if (item.ItemID == itemID)
                                {
                                    y = true;
                                }
                            }
                            if (y)
                            {
                                string dete = Extend.OnlyYN("Sản phẩm sẽ bị xóa?(Y/N): ");
                                if (dete == "Y")
                                {
                                    orderBL.DeleteItemInShoppingCartByItemID(itemID);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Không có sản phẩm này trong giỏ hàng");
                                Console.WriteLine("Nhấn phím bất kì để tiếp tục");
                                Console.ReadKey();
                            }
                            continue;
                    }
                    break;

                }
            }
        }

        public void CreateOrder(double total)
        {
            order.OrderUser = new User();
            OrderBL orderBL = new OrderBL();
            order.OrderUser = user;
            try
            {
                if (orderBL.CreateOrder(order))
                {
                    // Console.Clear();
                    userBL.UpdateStatusShoppingCartById(true, user.UserID);

                    List<Order> shoppingCart = new List<Order>();
                    shoppingCart = orderBL.ShowOrderUserPaySucess(user.UserID);

                    Console.WriteLine("Hóa Đơn");
                    Console.WriteLine("TÊN KHÁCH HÀNG: {0}", shoppingCart[0].OrderUser.UserName);
                    Console.WriteLine("EMAIL KHÁCH HÀNG: {0}", shoppingCart[0].OrderUser.UserEmail);

                    Console.WriteLine("MÃ ĐƠN HÀNG: {0}", shoppingCart[0].OrderID);
                    var table = new ConsoleTable("MÃ ĐỒ UỐNG", "TÊN ĐỒ UỐNG", "SIZE", "GIÁ");
                    foreach (var item in shoppingCart)
                    {
                        table.AddRow(item.OrderItem.ItemID, item.OrderItem.ItemName, item.OrderItem.ItemSize, FormatCurrency(item.OrderItem.ItemPrice));
                    }
                    table.AddRow("", "", "", "");
                    table.AddRow("TỔNG TIỀN", "","", FormatCurrency(total));
                    table.AddRow("NGÀY MUA", "","", shoppingCart[0].OrderDate?.ToString("yyyy-MM-dd"));
                    table.Write();

                    Console.WriteLine("CÁM ƠN QUÝ KHÁCH");
                    Console.WriteLine("HẸN GẶP LẠI");

                }
                else
                {
                    Console.WriteLine("Không thể mua hàng");
                }
            }
            catch (System.Exception)
            {

                throw;
            }

            Console.WriteLine("Bấm phím bất kỳ để tiếp tục");
            Console.ReadKey();
        }

        public void ShowOrder()
        {
            // Console.Clear();
            OrderBL orderBL = new OrderBL();
            List<Order> listOrder = new List<Order>();
            listOrder = orderBL.ShowAllItemOrder(user.UserID);

            if (listOrder == null)
            {
                Console.ReadKey();
            }
            else
            {
                if (listOrder.Count <= 0)
                {
                    Console.WriteLine("Bạn chưa mua gì");
                }
                else
                {
                    var table = new ConsoleTable("Mã đồ uống", "Tên đồ uống", "Ngày mua");
                    foreach (var item in listOrder)
                    {
                        table.AddRow(item.OrderItem.ItemID, item.OrderItem.ItemName, item.OrderDate?.ToString("yyyy-MM-dd"));
                    }
                    table.Write();
                }
                Console.WriteLine("Nhấn phím bất kì để tiếp tục");
                Console.ReadKey();
            }
        }
        public static string FormatCurrency(double ItemPrice)
        {
            string a = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", ItemPrice);
            return a;
        }


    }
}