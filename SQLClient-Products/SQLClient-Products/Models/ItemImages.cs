using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLClient_Products.Models
{
    public class ItemImages
    {
        public int ItemId { get; set; }

        public int ImageId { get; set; }
        public string ItemUrl { get; set; }

        public ItemImages()
        {

        }

        public ItemImages(int id, int imageId, string url)
        {
            this.ItemId = id;
            this.ImageId = imageId;
            this.ItemUrl = url;
        }
    }
}