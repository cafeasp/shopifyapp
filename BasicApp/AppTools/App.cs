using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;

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

    }
}