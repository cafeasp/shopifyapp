# shopifyapp
ASP.NET MVC 4 C# Project - Basic template to build a Shopify App. It should give you a good idea on how to create/setup your own project.
## Getting Started
Make sure your computer has git software and Visual Studio Community 2015 or better. Run this command 
```
git clone https://github.com/cafeasp/shopifyapp.git
```
Open the project with Visual Studio and make sure you **restore packages**

## Running a test
You have to join the Shopify Partner at https://app.shopify.com/services/partners/signup 
Once you have a account, create a app and get your app id and app secret info.
This project is using a config file to keep the app settings outside of the project for security.

## Create Config file
You can create this file anywhere in your computer.
```
For example: C:\myapp\app.config (make sure this file name and path is the same in your web.config)
```
```
web.config example:
<appSettings file="c:\MyAppSettings\app.config">
    <add key="webpages:Version" value="3.0.0.0" />
    <add key="webpages:Enabled" value="false" />
    <add key="ClientValidationEnabled" value="true" />
    <add key="UnobtrusiveJavaScriptEnabled" value="true" />
  </appSettings>
```
Your app.config - see below. You get app id / secret from shopify app page. You can use any domain you have available or use ngrok.com
to allow your app to be available on the public domain. ngrok.com has a free and paid version.
```
<?xml version="1.0" encoding="utf-8" ?>
<appSettings>
   <add key="AppId" value="your_app_id" />
   <add key="AppSecret" value="your_app_secret" />
   <add key="AppScope" value="read_orders,write_orders" />
   <add key="AppDomain" value="your_domain.com" />
   <add key="AppInstallControllerName" value="shopify" />
</appSettings>
```
