using System;
using Xunit;
using Persistence.MODEL;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace DAL.Test
{

    public class OrderUnitTest
    {
        private MySqlDataReader reader;
        OrderDAL orderDAL = new OrderDAL();
        [Fact]
        public void CreateShoppingCartTest()
        {
            UserDAL userDAL = new UserDAL();
            Order order = new Order();
            ItemDAL itemDAL = new ItemDAL();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.CartStatus = 0;
            order.OrderItem = itemDAL.GetItemByID(2);
            order.OrderUser = userDAL.GetUserById(1);


            Assert.True(orderDAL.CreateShoppingCart(order));
            orderDAL.DeleteAllItemInShoppingCartByUserID(1);
            // userDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserID); // set userShopping cart to 1
        }
        [Fact]
        public void CreateShoppingCartTest1()
        {
            UserDAL userDAL = new UserDAL();
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.CartStatus = 0;
            order.OrderUser.UserID = 0;
            order.OrderItem.ItemID = 0;

            Assert.False(orderDAL.CreateShoppingCart(order));
        }
        [Fact]
        public void AddToShoppingCartTest()
        {
            Order order = new Order();
            ItemDAL itemDAL = new ItemDAL();
            UserDAL userDAL = new UserDAL();
            order.OrderUser = new User();
            Item item = new Item();
            order.OrderItem = new Item();


            order.OrderItem = itemDAL.GetItemByID(9);
            order.OrderUser = userDAL.GetUserById(1);


            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,CartStatus) values ({order.OrderUser.UserID},0)";
            command.ExecuteNonQuery();
            userDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserID); // set userShopping cart to 1

            Assert.True(orderDAL.AddToShoppingCart(order));

            orderDAL.DeleteAllItemInShoppingCartByUserID(order.OrderUser.UserID);
            userDAL.UpdateStatusShoppingCartById(true, order.OrderUser.UserID); // set userShopping cart to 0
        }
        [Fact]
        public void AddToShoppingCartTest1()
        {
            Order order = new Order();
            ItemDAL itemDAL = new ItemDAL();
            order.OrderUser = new User();
            Item item = new Item();
            order.OrderItem = new Item();
            UserDAL userDAL = new UserDAL();


            order.OrderUser.UserID = 0;
            order.OrderItem.ItemID = 0;


            Assert.False(orderDAL.AddToShoppingCart(order));
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

            Assert.False(orderDAL.AddToShoppingCart(order));

            orderDAL.DeleteAllItemInShoppingCartByUserID(order.OrderUser.UserID);
            userDAL.UpdateStatusShoppingCartById(true, order.OrderUser.UserID); // set userShopping cart to 0
        }
        [Fact]
        public void DeleteItemInShoppingCartByItemIDTest()
        {
            int UserID = 1;
            int idItem = 1;
            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,CartStatus) values ({UserID},0)";
            command.ExecuteNonQuery();
            int orderID = GetLastInsertOrderID(1);
            command.CommandText = $"insert into OrderDetails(orderId,ItemID) values ({orderID},{idItem})";
            command.ExecuteNonQuery();

            Assert.True(orderDAL.DeleteItemInShoppingCartByItemID(UserID));
            orderDAL.DeleteAllItemInShoppingCartByUserID(UserID);
        }
        [Fact]
        public void DeleteItemInShoppingCartByItemIDTest1()
        {

            Assert.False(orderDAL.DeleteItemInShoppingCartByItemID(null));

        }
        [Fact]
        public void DeleteAllItemInShoppingCartByUserIDTest()
        {
            int UserID = 1;
            MySqlCommand command = DBHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,CartStatus) values ({UserID},0)";
            command.ExecuteNonQuery();
            int orderID = GetLastInsertOrderID(1);
            command.CommandText = $"insert into OrderDetails(orderId,ItemID) values ({orderID},1)";
            command.ExecuteNonQuery();

            Assert.True(orderDAL.DeleteAllItemInShoppingCartByUserID(UserID));
        }
        [Fact]
        public void DeleteAllItemInShoppingCartByUserIDTest1()
        {

            Assert.False(orderDAL.DeleteAllItemInShoppingCartByUserID(0));
        }

        [Fact]
        public void ShowShoppingCartByUserIDTest()
        {
            Assert.NotNull(orderDAL.ShowShoppingCartByUserID(1));
        }
        [Fact]
        public void ShowShoppingCartByUserIDTest1()
        {
            Assert.Null(orderDAL.ShowShoppingCartByUserID(null));
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

            Assert.True(orderDAL.CreateOrder(order));

            orderDAL.DeleteAllItemInShoppingCartByUserID(1);
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
            orderDAL.CreateShoppingCart(order);
            orderDAL.CreateOrder(order);

            Assert.NotNull(orderDAL.ShowAllItemOrder(1));

            orderDAL.DeleteAllItemInShoppingCartByUserID(1);
        }
        [Fact]
        public void ShowOrderByUserIDTest1()
        {
            Assert.Null(orderDAL.ShowAllItemOrder(null));
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
            orderDAL.CreateShoppingCart(order);
            orderDAL.CreateOrder(order);

            Assert.NotNull(orderDAL.ShowOrderUserPaySucess(1));

            orderDAL.DeleteAllItemInShoppingCartByUserID(1);
        }
        [Fact]
        public void ShowOrderUserPaySucessTest1()
        {
            Assert.Null(orderDAL.ShowOrderUserPaySucess(null));
        }
        public int GetLastInsertOrderID(int UserID)
        {
            int orderId = -1;

            string queryLastInsertId = $@"select orderId from orders where orderUser = {UserID} order by orderid desc limit 1;";
            reader = DBHelper.ExecQuery(queryLastInsertId, DBHelper.OpenConnection());
            if (reader.Read())
            {
                orderId = reader.GetInt32("orderId");

            }
            reader.Close();
            return orderId;
        }
    }
}
