using System;
using DAL;
using Persistence.MODEL;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BL
{
    public class ItemBL
    {
        private ItemDAL itemDAL;

        public ItemBL()
        {
            itemDAL = new ItemDAL();
        }

        public List<Item> GetListItems()
        {
            return itemDAL.GetListItems();
        }

        public List<Item> SearchItemName()
        {
            return itemDAL.SearchItemName();
        }

        public List<Item> SearchItemName(string itemName)
        {
            itemName = itemName.ToLower();
            Console.WriteLine(itemName);
            List<Item> items = new List<Item>();
            List<Item> newItems = new List<Item>();
            items = itemDAL.SearchItemName();

            foreach (var item in items)
            {
                if (item.ItemName.ToLower().Contains(itemName))
                {
                    newItems.Add(item);
                }
            }
            return newItems;
        }
        public Item GetItemByID(int? itemID)
        {
            if (itemID == null)
            {
                return null;
            }
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollectionID = regex.Matches(itemID.ToString());
            if (matchCollectionID.Count < itemID.ToString().Length)
            {
                return null;
            }
            return itemDAL.GetItemByID(itemID);
        }
    }
}