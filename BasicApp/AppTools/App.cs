using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;

namespace BasicApp.AppTools
{
    /// <summary>
    /// Your app settings
    /// </summary>
    public static class App
    {
        public static string AppId = ConfigurationManager.AppSettings["AppId"];
        public static string AppSecret = ConfigurationManager.AppSettings["AppSecret"];
        public static string AppScope = ConfigurationManager.AppSettings["AppScope"];
        public static string AppDomain = ConfigurationManager.AppSettings["AppDomain"];
        public static string AppInstallControllerName = ConfigurationManager.AppSettings["AppInstallControllerName"];

        public static string GetInstallState()
        {
            return Guid.NewGuid().ToString("N");
        }
        public static string GetRedirectUrl(string shop)
        {
            string redirectUrl =
                string.Format(
                "https://{0}/admin/oauth/authorize?client_id={1}&scope={2}&redirect_uri=https://{3}/{4}/auth&state={5}",
                shop,
                AppId,
                AppScope,
                AppDomain,
                AppInstallControllerName,
                GetInstallState());
            return redirectUrl;
        }
        public static bool AllowHostName(string shop)
        {
            
            Match match = Regex.Match(shop, @"^[a-z\d_.-]+myshopify.com$");
            return match.Success;
        }

        public static bool VerifyHmac(NameValueCollection queryString)
        {
            Func<string, bool, string> replaceChars = (string s, bool isKey) =>
            {
                //replace % before replacing &. Else second replace will replace those %25s.
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

            var hmacHasher = new HMACSHA256(Encoding.UTF8.GetBytes(App.AppSecret));
            var hash = hmacHasher.ComputeHash(Encoding.UTF8.GetBytes(string.Join("&", kvps)));

            //Convert bytes back to string, replacing dashes, to get the final signature.
            var calculatedSignature = BitConverter.ToString(hash).Replace("-", "");

            //Request is valid if the calculated signature matches the signature from the querystring.
            return calculatedSignature.ToUpper() == queryString.Get("hmac").ToUpper();
        }

        public static string GetAccessTokenUrl()
        {
            return "oauth/access_token";
        }
    }
}