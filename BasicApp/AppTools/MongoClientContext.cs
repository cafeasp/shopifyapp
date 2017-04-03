using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
namespace BasicApp.AppTools
{
    public class MongoClientContext
    {
        public IMongoDatabase Database;
        public MongoClientContext()
        {
            var client = new MongoClient("mongodb://localhost");
            Database = client.GetDatabase("myapp");
        }

        
    }
}