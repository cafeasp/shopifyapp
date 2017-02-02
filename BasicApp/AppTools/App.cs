using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace BasicApp.AppTools
{
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
    }
}