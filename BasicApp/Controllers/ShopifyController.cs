using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicApp.AppTools;
using System.Collections.Specialized;
using BasicApp.Models;
using Newtonsoft.Json;

namespace BasicApp.Controllers
{
    public class ShopifyController : Controller
    {
        //shopify client
        private ShopifyClient client;
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
            client = new ShopifyClient(shop);
            string token = client.GetToken(App.GetAccessTokenUrl(), code);
            //save token to db
            System.IO.File.WriteAllText("C:\\MyAppSettings\\token.txt", token);//for demo ONLY

            //allow customer to view our app
            //return RedirectToAction("Index", "AppPortal");

            //if you are charging for the app uncomment continue below
            string chargeJson = App.CreateChargeJson(shop);

            //Call Shopify with json - charge and check if it was created OK
            var Charge = client.CreateCharge(chargeJson,token);

            if(Charge.StatusCode == System.Net.HttpStatusCode.Created)
            {
                //DeserializeObject and save to db
                ChargeResultModel chargeResult = JsonConvert.DeserializeObject<ChargeResultModel>(Charge.Content);

                //send customer to approve or declined the charge
                return Redirect(chargeResult.recurring_application_charge.ConfirmationUrl);
            }
            else
            {
                //something is wrong
                //check errors 
                return RedirectToAction("ErrorPage", "Home");
            }

        }


        public ActionResult AuthResult(string shop,string charge_id)
        {
            //find out if the customer accepted the chanrge
            //pull up shop token from db
            string token = App.GetTokenDEMO(shop);
            client = new ShopifyClient(shop,token);
            var check = client.CheckChargeById(charge_id);
            var chargeStatus = JsonConvert.DeserializeObject<ChargeResultModel>(check.Content);
            if(chargeStatus.recurring_application_charge.Status == "accepted")
            {
                //customer accepted the charge
                //make sure you save this charge id and activated

                var active = client.ActivateChargeById(charge_id, App.CreateActiveJson(chargeStatus));
                if(active == System.Net.HttpStatusCode.OK)
                {
                    //every is good allow customer to view your app
                    return RedirectToAction("Index", "AppPortal");
                }else
                {
                    //activation fail
                    return RedirectToAction("ErrorPage", "Home");
                }

            }
            else
            {
                //customer declined or another error happen
                return RedirectToAction("ErrorPage", "Home");
            }

            
        }
    }
}