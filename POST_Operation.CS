//Problem->This Program Adds a Row to the List of a Sharepoint Site.
//Solution:-

using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Security;
using Microsoft.SharePoint.Client;
using System.Runtime.Remoting.Contexts;
using SP = Microsoft.SharePoint.Client;
using System.Collections.Generic;
//using Microsoft.Sharepoint.Client.QueryExpression;

namespace AddListData
{

    class Program
    {
        static void Main(string[] args)
        {
            string userName = "Write-Your-Office365-UserName-Within-these-Double-Quotes";
            Console.WriteLine("Enter your password");           
            SecureString password = GetPassword();
             
            //Client Context Gets Context of SharePoint Online Site     //Site URL
            using (var clientContext = new ClientContext("Write-Your-Sharepoint-Site-URL-Within-these-Double-Quotes")) 
            {
                //Sharepoint Online Credentials
                clientContext.Credentials = new SharePointOnlineCredentials(userName, password);

      
                try
                {
                     List Fetched_List = clientContext.Web.Lists.GetByTitle("Second_List");  // "Second_List" in the Name of my Sharepoint List 

                    //creating template ofItem that we are going to add
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem newItem = Fetched_List.AddItem(itemCreateInfo);

                    newItem["Title"] = "Pappu";
                    newItem["Your_Name"] = "Rahul Gandhi";
                    newItem["Roll_No"] = 65;
                    newItem["Subject"] = "Chota_Bheem";
                    newItem["MyField2"] = 68;

                    newItem.Update();

                    clientContext.ExecuteQuery();
                }
                 
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Occured" + ex.Message);
                }
            }
        }


        private static SecureString GetPassword()
        {
            ConsoleKeyInfo info;
            //Get the user's password as a SecureString
            SecureString securePassword = new SecureString();
            do
            {
                info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Enter)
                {
                    securePassword.AppendChar(info.KeyChar);
                }
            }
            while (info.Key != ConsoleKey.Enter);
            return securePassword;
        }
    }
}
