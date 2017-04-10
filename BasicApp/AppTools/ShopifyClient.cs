using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using Newtonsoft.Json;
using BasicApp.Models;

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
            Request.AddParameter("application/x-www-form-urlencoded", "client_id=" + App.Id + "&client_secret=" + App.Secret + "&code=" + code, ParameterType.RequestBody);

            Response = Client.Execute(Request);
            var r = JsonConvert.DeserializeObject<dynamic>(Response.Content);
            var accessToken = r.access_token;
            return (string)accessToken;
        }
        public IRestResponse CreateCharge(string chargeJson,string token)
        {
            var request = CreateRequest("recurring_application_charges.json", Method.POST);
            request.AddHeader("X-Shopify-Access-Token", token);
            request.AddParameter("application/json", chargeJson, ParameterType.RequestBody);
            var result = Client.Execute(request);
            return result;
        }
        public RestRequest CreateRequest(string endPoint,Method methodType)
        {
            var res = new RestRequest(endPoint,methodType);
            res.RequestFormat = DataFormat.Json;
            res.AddHeader("Content-Type", "application/json");
            return res;
        }

        public IRestResponse CheckChargeById(string charge_id)
        {
            var request = CreateRequest("recurring_application_charges/" + charge_id + ".json", Method.GET);
            request.AddHeader("X-Shopify-Access-Token", Token);
            return Client.Execute(request);
        }

        public IRestResponse GetProductCount()
        {
            var request = CreateRequest("products/count.json", Method.GET);
            return Client.Execute(request);
        }
        public IRestResponse GetProduct(int page=1,int limit=50)
        {
            var request = CreateRequest("products.json?page=" + page + "&limit=" + limit, Method.GET);
            return Client.Execute(request);
        }


        public System.Net.HttpStatusCode ActivateChargeById(string charge_id,string json)
        {
            var request = CreateRequest("recurring_application_charges/" + charge_id + "/activate.json", Method.POST);
            request.AddHeader("X-Shopify-Access-Token", Token);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            return Client.Execute(request).StatusCode;
        }

       
    }
}