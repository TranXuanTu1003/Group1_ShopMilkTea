using System;
using Persistence.MODEL;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class ItemDAL
    {
        private MySqlDataReader reader;
        private string query;
        public ItemDAL() { }
        public List<Item> SearchItemName()
        {
            DBHelper.OpenConnection();
            // query = $@"select * from Items limit 10;";
            query = $@"select it.itemID, it.itemName, it.itemNameEnglish, itdls.itemSize, itdls.itemPrice, it.itemPreview, it.itemResources, it.itemQuantity, it.itemDescription from Items it, ItemDetails itdls where it.itemID = itdls.itemID ; ";
            List<Item> items = new List<Item>();
            try
            {
                reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            }
            catch (System.Exception)
            {
                Console.WriteLine("Khong the connection voi database");
                return null;
            }
            while (reader.Read())
            {
                items.Add(GetItem(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return items;
        }
        public List<Item> GetListItems()
        {
            DBHelper.OpenConnection();
            // query = $@"select * from Items;";
            query = $@"select it.itemID, it.itemName, it.itemNameEnglish, itdls.itemSize, itdls.itemPrice, it.itemPreview, it.itemResources, it.itemQuantity, it.itemDescription from Items it, ItemDetails itdls where it.itemID = itdls.itemID limit 10; ";
            List<Item> items = new List<Item>();
            try
            {
                reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            }
            catch (System.Exception)
            {
                Console.WriteLine("Ko the connection voi database");
                return null;
            }
            while (reader.Read())
            {
                items.Add(GetItem(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return items;
        }
        public Item GetItemByID(int? itemID)
        {
            if (itemID == null)
            {
                return null;
            }

            query = $@"select it.itemID, it.itemName, it.itemNameEnglish, it.itemQuantity, it.itemDescription, it.itemPreview, it.itemResources ,itdls.itemSize, itdls.itemPrice from Items it, ItemDetails itdls where it.itemID = " + itemID + ";";
            // query = $"select * from Items where itemID = {itemID}";
            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            Item item = null;
            if (reader.Read())
            {
                item = GetItem(reader);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return item;
        }

        public Item GetItemBySize(string itemSize)
        {
            if (itemSize == null)
            {
                return null;
            }

            query = $@"select it.itemID, it.itemName, it.itemNameEnglish, it.itemQuantity, it.itemDescription, it.itemPreview, it.itemResources ,itdls.itemSize, itdls.itemPrice from Items it, ItemDetails itdls where itdls.itemSize = " + itemSize + ";";
            // query = $"select * from Items where itemID = {itemID}";
            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            Item item = null;
            if (reader.Read())
            {
                item = GetItem(reader);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return item;
        }


        public List<Item> SearchItem(int temp)
        {
            DBHelper.OpenConnection();
            switch (temp)
            {
                case 1:
                    // query = $@"select * from Items where itemID = ";
                    query = $@"select it.itemID, it.itemName, it.itemNameEnglish, it.itemQuantity, it.itemDescription, it.itemPreview, it.itemResources ,itdls.itemSize, itdls.itemPrice, from Items it, ItemDetails itdls where itemID = ;";

                    break;
            }

            reader = DBHelper.ExecQuery(query, DBHelper.OpenConnection());
            List<Item> items = new List<Item>();
            while (reader.Read())
            {
                items.Add(GetItem(reader));
            }
            reader.Close();
            DBHelper.CloseConnection();
            return items;
        }
        private Item GetItem(MySqlDataReader reader)
        {
            Item item = new Item();
            item.ItemID = reader.GetInt32("itemID");
            item.ItemName = reader.GetString("itemName");
            item.ItemNameEnglish = reader.GetString("itemNameEnglish");
            item.ItemPreview = reader.GetString("itemPreview");
            item.ItemPrice = reader.GetInt32("itemPrice");
            item.ItemQuantity = reader.GetInt32("itemQuantity");
            item.ItemSize = reader.GetString("itemSize");
            item.ItemResources = reader.GetString("itemResources");
            return item;
        }
    }
}