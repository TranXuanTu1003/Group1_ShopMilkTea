using  System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence.MODEL;

namespace DAL{
    public class OrderDAL{
        private MySqlDataReader reader;
        private string query;
        public OrderDAL(){}
        public bool CreateShoppingCart(Order order){
            bool result = false;
            if(order == null){
                return result;
            }

            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = @"lock tables Orders write, Items write, OrderDetails write, ItemDetails write";
            command.ExecuteNonQuery();

            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "insert into Orders(orderUser, cartStatus) values(@userID,@CartStatus);";
                command.Parameters.AddWithValue("@userID",order.OrderUser.UserID);
                command.Parameters.AddWithValue("@cartStatus", order.CartStatus);
                command.ExecuteNonQuery();

                string queryLastInsertID = $@"select orderID from Orders where orderUser = {order.OrderUser.UserID} order by orderID desc limit 1;";
                MySqlCommand selectLastID = new MySqlCommand(queryLastInsertID, connection);
                using (reader = selectLastID.ExecuteReader())
                {
                    if(reader.Read()){
                        order.OrderID = reader.GetInt32("orderID");
                    }
                }

                command.Parameters.Clear();
                command.CommandText = "insert into OrderDetails(orderID, itemID) values (@orderID, @itemID);";
                command.Parameters.AddWithValue("@orderID", order.OrderID);
                command.Parameters.AddWithValue("@itemID",order.OrderItem.ItemID);
                command.ExecuteNonQuery();

                transaction.Commit();
                result = true;
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex);
                return result;   
            }
            finally{
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Close();
            }

            return result;

        }

        public bool AddToShoppingCart(Order order){
            bool result = false;

            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = @"lock tables Orders write, Items write, OrderDetails write, ItemDetails write";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            string queryLastInsertID = $@"select orderID from Orders where orderUser = {order.OrderUser.UserID} order by orderID desc limit 1;";
            MySqlCommand selectLastID = new MySqlCommand(queryLastInsertID, connection);
            using (reader = selectLastID.ExecuteReader())
            {
                if(reader.Read()){
                    order.OrderID = reader.GetInt32("orderID");
                }
            }
            try
            {
                command.CommandText = "insert into OrderDetails(orderID,itemID) values (@orderID, @itemID);";
                command.Parameters.AddWithValue("@orderID", order.OrderID);
                command.Parameters.AddWithValue("@itemID",order.OrderItem.ItemID);
                command.ExecuteNonQuery();
                transaction.Commit();
                result = true;
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                return result;
            }
            finally{
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Close();
            }

            return result;
        }

        public bool DeleteItemInShoppingCartByItemID(int? itemID){
            if(itemID == null){
                return false;
            }
            query = $@"delete from OrderDetails where itemID = {itemID}; ";

            MySqlConnection connection = DBHelper.OpenConnection();
            if(DBHelper.ExecNonQuery(query, connection) == 0){
                DBHelper.CloseConnection();
                return false;
            }
            DBHelper.CloseConnection();
            return true;
        }

        public bool DeleteAllItemInShoppingCartByUserID(int? userID){
            bool result = false;
            int orderID = -1;
            MySqlConnection connection = DBHelper.OpenConnection();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"lock tables Users write, Orders write, Items write, OrderDetails write, ItemDetails write";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            try
            {
                string queryLastInsertID = $@"select max(orderID) as orderID from Orders where orderUser = {userID} order by orderID desc limit 1;";
                MySqlCommand selectLastID = new MySqlCommand(queryLastInsertID, connection);
                using (reader = selectLastID.ExecuteReader())
                {
                    if(reader.Read()){
                        orderID = reader.GetInt32("orderID");
                    }
                }
                command.Parameters.Clear();
                command.CommandText = $@"delete from OrderDetails where orderID = {orderID};";
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                command.CommandText = $@"delete from Orders where orderID = {orderID};";
                command.ExecuteNonQuery();

                transaction.Commit();
                result = true;
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex);
                return result;
            }
            finally
            {
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Clone();
            }

            return result;
        }

        public List<Item> ShowShoppingCartByUserID(int? userID){
            if(userID == null){
                return null;
            }

            List<Item> listItems = new List<Item>();
            query = $@"select it.itemID, it.itemName, itdls.itemPrice from Orders ord inner join OrderDetails ordls on ord.orderID = ordls.orderID 
                       inner join Items it on ordls.itemID = it.itemID 
                       inner join ItemDetails itdls on itdls.itemID = it.itemID
                       where ord.orderUser = {userID} and ord.cartStatus = 0 ;";
            
            try
            {
                reader = DBHelper.ExecQuery(query,DBHelper.OpenConnection());
            }
            catch (System.Exception)
            {
                Console.WriteLine("Không thể kết nối với cơ sở dữ liệu !!!.");
                return null;
            }
            while (reader.Read())
            {
                listItems.Add(GetItemShoppingCart(reader));
            }
            DBHelper.CloseConnection();
            return listItems;
        }

        public bool CreateOrder(Order order){
            bool result = false;
            if(order == null){
                return result;
            }
            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"lock tables Users write, Orders write, Items write, OrderDetails write, ItemDetails write";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            try
            {
                string queryLastInsertID = $@"select orderID from Orders where orderUser = {order.OrderUser.UserID} order by orderID desc limit 1;";
                MySqlCommand selectLastID = new MySqlCommand(queryLastInsertID, connection);
                using(reader = selectLastID.ExecuteReader()){
                    if(reader.Read()){
                        order.OrderID = reader.GetInt32("orderID");
                    }
                }

                command.Parameters.Clear();
                command.CommandText = $@"update Orders set cartStatus = 1, orderDate = now() where orderUser = {order.OrderUser.UserID} and orderID = {order.OrderID};";
                command.ExecuteNonQuery();

                transaction.Commit();
                result = true;
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex);
                return result;
            }
            finally
            {
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Clone();
            }

            return result;
        }

        public List<Order> ShowAllItemOrder(int? userID){
            if(userID == null){
                return null;
            }
            List<Order> listOrders = new List<Order>();
            query = $@"select it.itemID, it.itemName, ord.orderDate from Orders ord inner join OrderDetails ordls on ord.orderID = ordls.orderID 
                       inner join Items it on ordls.itemID = it.itemID where ord.orderUser = {userID} and ord.cartStatus = 1 group by it.itemName ;";

            try
            {
                reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            }
            catch (System.Exception)
            {
                Console.WriteLine("Ko the ket noi voi database");
                return null;
            }
            while (reader.Read())
            {
                listOrders.Add(GetOrder(reader));
            }
            DBHelper.CloseConnection();
            return listOrders;
        }

        public Order GetLastOrderIDPurchase(int? userID){
            if(userID == null){
                return null;
            }

            Order order = null;
            query =$@"select max(orderID) from Orders where orderUser = {userID} ;";
            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            if(reader.Read()){
                order = GetOrder(reader);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return order;
        }

        public List<Order> ShowOrderUserPaySucess(int? userID){
            if(userID == null){
                return null;
            }

            List<Order> orders = new List<Order>();
            query = $@"select ord.orderID as orderID, ord.orderDate, it.itemID, it.itemName, itdls.itemPrice, us.userName, us.userEmail from Users us 
                       inner join Orders ord on ord.orderUser = us.userID 
                       inner join orderDetails ordls on ord.orderID = ordls.orderID
                       inner join Items it on ordls.itemID = it.itemID
                       inner join ItemDetails itdls on itdls.itemID = it.itemID
                       where ord.orderUser = {userID} and ord.orderID = {GetLastInsertOrderID(userID)} ;";

            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            while (reader.Read())
            {
                orders.Add(GetOrderPurchaseSucess(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return orders;
        }

        public int GetLastInsertOrderID(int? userID){
            int orderID = -1;

            string queryLastInsertID = $@"select orderID from Orders where orderUser = {userID} order by orderID desc limit 1;";
            reader = DBHelper.ExecQuery(queryLastInsertID, DBHelper.OpenConnection());
            if(reader.Read()){
                orderID = reader.GetInt32("orderID");
            }
            reader.Close();
            return orderID;
        }

        public int? CheckItemPurchase(int? itemID, int? userID){
            string query = $@"select it.itemID from Orders ord inner join OrderDetails ordls on ord.orderID = ordls.orderID
                             where it.itemID = {itemID} and ord.orderUser = {userID} and ord.cartStatus = 1 limit 1;";
                            
            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            if(reader.Read()){
                itemID = reader.GetInt32("itemID");
            }
            else{
                itemID = -1;
            }
            reader.Close();
            return itemID;
        }

        private Item GetItemShoppingCart(MySqlDataReader reader){
            Item item = new Item();
            item.ItemID = reader.GetInt32("itemID");
            item.ItemName = reader.GetString("itemName");
            item.ItemPrice = reader.GetDouble("itemPrice");
            // item.ItemNameEnglish = reader.GetString("itemNameEnglish");
            return item;
        }

        private Order GetOrder(MySqlDataReader reader){
            Order order = new Order();
            order.OrderItem = new Item();
            order.OrderItem.ItemID = reader.GetInt32("itemID");
            order.OrderItem.ItemName = reader.GetString("itemName");
            order.OrderDate = reader.GetDateTime("orderDate");

            return order;
        }

        private Order GetOrderPurchaseSucess(MySqlDataReader reader){
            Order order = new Order();
            order.OrderItem = new Item();
            order.OrderUser = new User();
            order.OrderID = reader.GetInt32("itemID");
            order.OrderUser.UserName = reader.GetString("userName");
            order.OrderUser.UserEmail = reader.GetString("userEmail");
            order.OrderItem.ItemID = reader.GetInt32("itemID");
            order.OrderItem.ItemPrice = reader.GetDouble("itemPrice");
            order.OrderDate = reader.GetDateTime("orderDate");
            order.OrderItem.ItemName = reader.GetString("itemName");

            return order;
        }

    }
}