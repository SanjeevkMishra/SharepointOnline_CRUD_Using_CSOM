//Problem-> This Program Deletes an Item from Sharepoint List based on Its ID.
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
                    //Creating template for the list to be deleted
                    List Fetched_List = clientContext.Web.Lists.GetByTitle("Second_List");
                    ListItem listItem = Fetched_List.GetItemById(1);

                    //Deleting list Item                    
                    listItem.DeleteObject();

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

