using System;

namespace Persistence.MODEL{
    public class Item{
        public Item(){}

        public Item(int? itemID,string itemName,string itemNameEnglish,int itemQuantity,string itemDescription,string itemPreview,string itemResources,string itemSize,double itemPrice){
            this.ItemID = itemID;
            this.ItemName = itemName;
            this.ItemNameEnglish = itemNameEnglish;
            this.ItemQuantity = itemQuantity;
            this.ItemDescription = itemDescription;
            this.ItemPreview = itemPreview;
            this.ItemResources = itemResources;
            this.ItemSize = itemSize;
            this.ItemPrice = itemPrice;

        }
        public int? ItemID{get;set;}
        public string ItemName{get;set;}
        public string ItemNameEnglish{get;set;}
        public int ItemQuantity{get;set;}
        public string ItemDescription{get;set;}
        public string ItemPreview{get;set;}
        public string ItemResources{get;set;}
        public string ItemSize{get;set;}
        public double ItemPrice{get;set;}
    }
}