using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicApp.AppTools;
namespace BasicApp.Controllers
{
    public class ShopifyController : Controller
    {
        //Step 1 - Shop wants to install app
        public ActionResult Install(string shop)
        {
            //Note: In live production - check if this shop has already install the app - check other settings in case your app is not free - check for charge id is active/valid

            //build redirect url
            string redirectUrl = 
                string.Format(
                "https://{0}/admin/oauth/authorize?client_id={1}&scope={2}&redirect_uri=https://{3}/{4}/auth&state={5}",
                shop,
                App.AppId,
                App.AppScope,
                App.AppDomain,
                App.AppInstallControllerName,
                App.GetInstallState());

            //Save GetInstallState value
            var state = HttpUtility.ParseQueryString(redirectUrl).Get("state");
            //save to database

            return Redirect(redirectUrl);
        }

    }
}