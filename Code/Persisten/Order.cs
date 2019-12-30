using System;
using System.Collections.Generic;

namespace Persistence.MODEL
{
    public class Order
    {
        public Order() { }
        public Order(int? orderID, User orderUser, DateTime? orderDate, DateTime? orderPaidDate, Item orderItem, List<Item> listItems, int count)
        {
            this.OrderID = orderID;
            this.OrderUser = orderUser;
            this.OrderDate = orderDate;
            this.OrderPaidDate = orderPaidDate;
            this.OrderItem = orderItem;
            this.ListItems = listItems;
            this.Count = count;

        }
        public int? OrderID { get; set; }
        public User OrderUser { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? OrderPaidDate { get; set; }
        public Item OrderItem ;
        public List<Item> ListItems;

        public int Count { get; set; }
    }
}