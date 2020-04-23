//Problem:- This Program Retrieves all the items from the Sharepoint List and Print them on The Console.
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
                     List Fetched_List = clientContext.Web.Lists.GetByTitle("Second_List");

                    // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll"
                    // so that it grabs all list items, regardless of the folder they are in.
                    CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                    ListItemCollection items = Fetched_List.GetItems(query);

                    // Retrieve all items in the ListItemCollection from List.GetItems(Query).
                    clientContext.Load(items);
                    clientContext.ExecuteQuery();

                    Console.WriteLine("Title" + "\t" + "Your_Name" + "\t" + "Id" + "\t" + "Subject" + "\t" + "MyField2");
                    foreach (ListItem listItem in items)
                    {
                        Console.WriteLine(listItem["Title"] + "\t" + listItem["Your_Name"] + "\t" + listItem["Roll_No"] + "\t" + listItem["Subject"] + "\t" + listItem["MyField2"]);
                    }
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

                 
                
