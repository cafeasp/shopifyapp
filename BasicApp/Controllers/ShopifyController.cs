using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicApp.AppTools;
using System.Collections.Specialized;

namespace BasicApp.Controllers
{
    public class ShopifyController : Controller
    {
        //Step 1 - Shop wants to install app
        public ActionResult Install(string shop)
        {
            //Note: In live production - check if this shop has already install the app - check other settings in case your app is not free - check for charge id is active/valid

            //build redirect url
            string redirectUrl = App.GetRedirectUrl(shop);

            //Save GetInstallState value
            var state = HttpUtility.ParseQueryString(redirectUrl).Get("state");
            //save state to database - later you will need to compare value for security reasons

            return Redirect(redirectUrl);
        }

        //Step 2
        public ActionResult Auth(string shop,string code,string state)
        {
            //security check 1
            //compare state value with db value save on install call
            //if value is not the same stop and exit

            //security check 2
            //valid hostname - ends with myshopify.com and does not contain characters other than letters (a-z), numbers (0-9), dots, and hyphens.
            if (!App.AllowHostName(shop))
            {
                //exit - send this request home
                return RedirectToAction("Index", "Home");        
            }
          
            //security check 3
            if (!App.VerifyHmac(Request.QueryString))
            {
                //exit
                return RedirectToAction("Index", "Home");
            }

            //make a call to Shopify and get back shop token
            string token = new ShopifyClient(shop).GetToken(App.GetAccessTokenUrl(), code);

            //save token to db

            //allow customer to view our app
            return RedirectToAction("Index", "AppPortal");
        }

        
    }
}