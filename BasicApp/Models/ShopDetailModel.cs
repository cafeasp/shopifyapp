using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicApp.Models
{
    public class ShopDetailModel
    {
        public ObjectId _id { get; set; }
        public string Shop { get; set; }
        public string State { get; set; }

    }
}