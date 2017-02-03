using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using Newtonsoft.Json;

namespace BasicApp.AppTools
{
    public class ShopifyClient
    {
        public string Shop { get; set; }
        public string Token { get; set; }
        private RestClient Client;
        private string AdminUrl;
        private IRestResponse Response;
        private RestRequest Request;
        public ShopifyClient(string shop,string token)
        {
            Shop = shop;
            Token = token;
            AdminUrl = string.Format("https://{0}/admin/", shop);
            Client = new RestClient(AdminUrl);
        }
        public ShopifyClient(string shop)
        {
            Shop = shop;
            AdminUrl = string.Format("https://{0}/admin/", shop);
            Client = new RestClient(AdminUrl);
        }
        public string GetToken(string resource,string code)
        {
            Request = CreateRequest(resource, Method.POST);
            //add other base on call           
            Request.AddParameter("application/x-www-form-urlencoded", "client_id=" + App.AppId + "&client_secret=" + App.AppSecret + "&code=" + code, ParameterType.RequestBody);

            Response = Client.Execute(Request);
            var r = JsonConvert.DeserializeObject<dynamic>(Response.Content);
            var accessToken = r.access_token;
            return (string)accessToken;
        }

        public RestRequest CreateRequest(string endPoint,Method methodType)
        {
            var res = new RestRequest(endPoint,methodType);
            res.RequestFormat = DataFormat.Json;
            res.AddHeader("Content-Type", "application/json");
            return res;
        }

    }
}