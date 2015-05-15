using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLClient_Products.Models
{
    public class Product
    {

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double ItemPrice { get; set; }
        public string ItemUrl { get; set; }


        public Product()
        {

        }

        public Product (int itemId, string itemName, string itemDescription, double itemPrice, string itemUrl)
        {
            this.ItemId = itemId;
            this.ItemName = itemName;
            this.ItemDescription = itemDescription;
            this.ItemPrice = itemPrice;
            this.ItemUrl = ItemUrl;
        }

        //TODO: fill in the product class. 
        // It should have at least the following properties:
        //     Id, Name, Description, Price, ImageUrl
    }
}