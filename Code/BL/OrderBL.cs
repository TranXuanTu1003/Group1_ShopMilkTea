using System;
using DAL;
using Persistence.MODEL;
using System.Collections.Generic;

namespace BL{
    public class OrderBL{
        OrderDAL orderDAL;

        public OrderBL(){
            orderDAL = new OrderDAL();
        }
        public bool CreateShoppingCart(Order order){
            if(order == null){
                return false;
            }
            return orderDAL.CreateShoppingCart(order);
        }

        public bool AddToShoppingCart(Order order){
            return orderDAL.AddToShoppingCart(order);
        }
        public bool DeleteItemInShoppingCartByItemID(int? itemID){
            return orderDAL.DeleteItemInShoppingCartByItemID(itemID);
        }

        public List<Item> ShowShoppingCartByUserID(int? userID){
            return orderDAL.ShowShoppingCartByUserID(userID);
        }

        public bool CreateOrder(Order order){
            return orderDAL.CreateOrder(order);
        }

        public List<Order> ShowAllItemOrder(int? userID){
            return orderDAL.ShowAllItemOrder(userID);
        }

        public List<Order> ShowOrderUserPaySucess(int? userID){
            if(userID == null){
                return null;
            }
            List<Order> listOrders = orderDAL.ShowOrderUserPaySucess(userID);
            foreach(var item in listOrders){
                item.OrderItem.ItemName.ToUpper();
            }
            return listOrders;
        }

        public bool DeleteAllItemInShoppingCartByUserID(int? userID){
            return orderDAL.DeleteAllItemInShoppingCartByUserID(userID);
        }

        // public int? CheckItemPurchase(int? itemID, int? userID){
        //     return orderDAL.CheckItemPurchase(itemID, userID);
        // }

    }
}