using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using BasicApp.Models;
using System.Threading.Tasks;

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

        public bool SaveInstallState(string shop,string state)
        {
            try
            {
                var collection = Database.GetCollection<ShopDetailModel>("InstallShop");
                collection.InsertOne(new ShopDetailModel { Shop = shop, State = state });
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
            
        }
    }
}