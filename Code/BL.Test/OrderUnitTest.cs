using System;
using Xunit;
using Persistence.MODEL;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BL;
namespace DAL.Test
{

    public class OrderUnitTest
    {
        private MySqlDataReader reader;
        private OrderBL orderBL = new OrderBL();
        private OrderDAL orderDAL = new OrderDAL();
        [Fact]
        public void CreateShoppingCartTest()
        {
            UserBL userBL = new UserBL();
            ItemBL itemBL = new ItemBL(); 
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.CartStatus = 0;
            order.OrderItem = itemBL.GetItemByID(2);
            order.OrderUser = userBL.GetUserById(1);


            Assert.True(orderBL.CreateShoppingCart(order));
            orderBL.DeleteAllItemInShoppingCartByUserID(1);
            // userDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserID); // set userShopping cart to 1
        }
        [Fact]
        public void CreateShoppingCartTest1()
        {
            UserBL userBL = new UserBL();
            ItemBL itemBL = new ItemBL(); 
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.CartStatus = 0;
            order.OrderItem = itemBL.GetItemByID(0);
            order.OrderUser = userBL.GetUserById(0);

            Assert.False(orderBL.CreateShoppingCart(order));
        }
        [Fact]
        public void AddToShoppingCartTest()
        {
            Order order = new Order();
            UserDAL userDAL = new UserDAL();
            order.OrderUser = new User();
            Item item = new Item();
            order.OrderItem = new Item();

            

            order.OrderUser.UserID = 1;
            order.OrderItem.ItemID = 4;


            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,CartStatus) values ({order.OrderUser.UserID},0)";
            command.ExecuteNonQuery();
            userDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserID); // set userShopping cart to 1

            Assert.True(orderBL.AddToShoppingCart(order));

            orderBL.DeleteAllItemInShoppingCartByUserID(order.OrderUser.UserID);
            userDAL.UpdateStatusShoppingCartById(true, order.OrderUser.UserID); // set userShopping cart to 0
        }
        [Fact]
        public void AddToShoppingCartTest1()
        {
            Order order = new Order();
            order.OrderUser = new User();
            Item item = new Item();
            order.OrderItem = new Item();


            order.OrderUser.UserID = 0;
            order.OrderItem.ItemID = 0;

            Assert.False(orderBL.AddToShoppingCart(order));
        }
        [Fact]
        public void AddToShoppingCartTest2()
        {
            Order order = new Order();
            UserDAL userDAL = new UserDAL();
            order.OrderUser = new User();
            Item item = new Item();
            order.OrderItem = new Item();


            order.OrderUser.UserID = 1;
            order.OrderItem.ItemID = null;


            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,CartStatus) values ({order.OrderUser.UserID},0)";
            command.ExecuteNonQuery();
            userDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserID); // set userShopping cart to 1

            Assert.False(orderBL.AddToShoppingCart(order));

            orderBL.DeleteAllItemInShoppingCartByUserID(order.OrderUser.UserID);
            userDAL.UpdateStatusShoppingCartById(true, order.OrderUser.UserID); // set userShopping cart to 0
        }
        [Fact]
        public void DeleteItemInShoppingCartByIdItemTest()
        {
            int UserID = 1;
            int idItem = 1;
            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,CartStatus) values ({UserID},0)";
            command.ExecuteNonQuery();
            int orderID = GetLastInsertorderID(1);
            command.CommandText = $"insert into OrderDetails(orderID,ItemID) values ({orderID},{idItem})";
            command.ExecuteNonQuery();

            Assert.True(orderBL.DeleteItemInShoppingCartByItemID(UserID));
            orderBL.DeleteAllItemInShoppingCartByUserID(UserID);
        }
        [Fact]
        public void DeleteItemInShoppingCartByIdItemTest1()
        {

            Assert.False(orderBL.DeleteItemInShoppingCartByItemID(null));

        }
        [Fact]
        public void DeleteAllItemInShoppingCartByUserIDTest()
        {
            int UserID = 1;
            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,CartStatus) values ({UserID},0)";
            command.ExecuteNonQuery();
            int orderID = GetLastInsertorderID(1);
            command.CommandText = $"insert into OrderDetails(orderID,ItemID) values ({orderID},1)";
            command.ExecuteNonQuery();

            Assert.True(orderBL.DeleteAllItemInShoppingCartByUserID(UserID));
        }
        [Fact]
        public void DeleteAllItemInShoppingCartByUserIDTest1()
        {

            Assert.False(orderBL.DeleteAllItemInShoppingCartByUserID(0));
        }

        [Fact]
        public void ShowShopingCartByUserIDTest()
        {
            Assert.NotNull(orderBL.ShowShoppingCartByUserID(1));
        }
        [Fact]
        public void ShowShopingCartByUserIDTest1()
        {
            Assert.Null(orderBL.ShowShoppingCartByUserID(null));
        }
        [Fact]
        public void CreateOrderTest()
        {
            UserDAL userDAL = new UserDAL();
            ItemDAL itemDAL = new ItemDAL();
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.CartStatus = 0;
            order.OrderItem = itemDAL.GetItemByID(2);
            order.OrderUser = userDAL.GetUserById(1);
            orderDAL.CreateShoppingCart(order);

            Assert.True(orderBL.CreateOrder(order));

            orderBL.DeleteAllItemInShoppingCartByUserID(1);


        }

        [Fact]
        public void ShowOrderByUserIDTest()
        {
            UserDAL userDAL = new UserDAL();
            ItemDAL itemDAL = new ItemDAL();
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.CartStatus = 0;
            order.OrderItem = itemDAL.GetItemByID(2);
            order.OrderUser = userDAL.GetUserById(1);
            orderBL.CreateShoppingCart(order);
            orderBL.CreateOrder(order);

            Assert.NotNull(orderBL.ShowAllItemOrder(1));

            orderBL.DeleteAllItemInShoppingCartByUserID(1);
        }
        [Fact]
        public void ShowOrderByUserIDTest1()
        {
            Assert.Null(orderBL.ShowAllItemOrder(null));
        }
        [Fact]
        public void ShowOrderUserPaySucessTest()
        {
            UserDAL userDAL = new UserDAL();
            ItemDAL itemDAL = new ItemDAL();
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.CartStatus = 0;
            order.OrderItem = itemDAL.GetItemByID(2);
            order.OrderUser = userDAL.GetUserById(1);

            orderBL.CreateShoppingCart(order);
            orderBL.CreateOrder(order);

            Assert.NotNull(orderBL.ShowOrderUserPaySucess(1));

            orderBL.DeleteAllItemInShoppingCartByUserID(1);
        }
        [Fact]
        public void ShowOrderUserPaySucessTest1()
        {
            Assert.Null(orderBL.ShowOrderUserPaySucess(null));
        }
        public int GetLastInsertorderID(int UserID)
        {
            int orderID = -1;

            string queryLastInsertId = $@"select orderID from orders where orderUser = {UserID} order by orderID desc limit 1;";
            reader = DBHelper.ExecQuery(queryLastInsertId, DBHelper.OpenConnection());
            if (reader.Read())
            {
                orderID = reader.GetInt32("orderID");

            }
            reader.Close();
            return orderID;
        }
    }
}
