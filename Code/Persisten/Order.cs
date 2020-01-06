using System;
using System.Collections.Generic;

namespace Persistence.MODEL
{
    public class Order
    {
        public Order() { }
        public Order(int? orderID, User orderUser,int cartStatus, DateTime? orderDate, Item orderItem, List<Item> listItems)
        {
            this.OrderID = orderID;
            this.OrderUser = orderUser;
            this.CartStatus = cartStatus;
            this.OrderDate = orderDate;
            this.OrderItem = orderItem;
            this.ListItems = listItems;
            

        }
        public int? OrderID { get; set; }
        public User OrderUser { get; set; }
        public int CartStatus{get;set;}
        public DateTime? OrderDate { get; set; }
        public Item OrderItem ;
        public List<Item> ListItems;
    }
}