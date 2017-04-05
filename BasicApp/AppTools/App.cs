using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using BasicApp.Models;
using Newtonsoft.Json;

namespace BasicApp.AppTools
{
    /// <summary>
    /// Your app settings save in app.config file. You can also save these in a database.
    /// </summary>
    public static class App
    {
        public static string Id = ConfigurationManager.AppSettings["Id"];
        public static string Secret = ConfigurationManager.AppSettings["Secret"];
        public static string Scope = ConfigurationManager.AppSettings["Scope"];
        public static string Domain = ConfigurationManager.AppSettings["Domain"];
        public static string InstallControllerName = ConfigurationManager.AppSettings["InstallControllerName"];
        public static string Name = ConfigurationManager.AppSettings["Name"];
        public static string Price = ConfigurationManager.AppSettings["Price"];
        public static string TrialDays = ConfigurationManager.AppSettings["TrialDays"];
        public static string Test = ConfigurationManager.AppSettings["Test"];
        

        public static string GetInstallState()
        {
            return Guid.NewGuid().ToString("N");
        }
        public static string GetRedirectUrl(string shop)
        {
            string redirectUrl =
                string.Format(
                "https://{0}/admin/oauth/authorize?client_id={1}&scope={2}&state={5}&redirect_uri=https://{3}/{4}/auth",
                shop,
                Id,
                Scope,
                Domain,
                InstallControllerName,
                GetInstallState());
            return redirectUrl;
        }
        public static bool AllowHostName(string shop)
        {
            
            Match match = Regex.Match(shop, @"^[a-z\d_.-]+[.]myshopify[.]com$");
            return match.Success;
        }

        public static bool VerifyHmac(NameValueCollection queryString)
        {
            Func<string, bool, string> replaceChars = (string s, bool isKey) =>
            {
                //replace % before replacing &. else second replace will replace those %25s.
                string output = (s?.Replace("%", "%25").Replace("&", "%26")) ?? "";

                if (isKey)
                {
                    output = output.Replace("=", "%3D");
                }

                return output;
            };

            var kvps = queryString.Cast<string>()
               .Select(s => new { Key = replaceChars(s, true), Value = replaceChars(queryString[s], false) })
               .Where(kvp => kvp.Key != "signature" && kvp.Key != "hmac")
               .OrderBy(kvp => kvp.Key)
               .Select(kvp => $"{kvp.Key}={kvp.Value}");

            var hmacHasher = new HMACSHA256(Encoding.UTF8.GetBytes(Secret));
            var hash = hmacHasher.ComputeHash(Encoding.UTF8.GetBytes(string.Join("&", kvps)));

            var calculatedSignature = BitConverter.ToString(hash).Replace("-", "");
            
            return calculatedSignature.ToUpper() == queryString.Get("hmac").ToUpper();
        }

        public static string GetAccessTokenUrl()
        {
            return "oauth/access_token";
        }
        public static string CreateChargeJson(string shop)
        {
            var charge = new ChargeModel();
            charge.Name = Name;
            charge.Price = Price;
            charge.ReturnUrl = string.Format("https://{0}/{1}/AuthResult?shop={2}", Domain,InstallControllerName,shop);
            charge.Test = Test;
            charge.TrialDays = TrialDays;

            var c = new { recurring_application_charge = charge };
            return JsonConvert.SerializeObject(c, Formatting.Indented);
        }
        public static string CreateActiveJson(ChargeResultModel model)
        {
            var updateModel = model;
            updateModel.recurring_application_charge.ConfirmationUrl = null;
            return JsonConvert.SerializeObject(updateModel, Formatting.Indented);
        }

        //you should get this value from db not a text file
        //this is only for demo
        public static string GetTokenDEMO(string shop)
        {           
            //call your db with the specific shop to pull the correct token
            return System.IO.File.ReadAllText("C:\\MyAppSettings\\token.txt");//this is only for demo
        }
    }
}
