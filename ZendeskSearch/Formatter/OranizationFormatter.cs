using System;
using System.Collections.Generic;
using System.Linq;
using ZendeskSearch.Models;

namespace ZendeskSearch.Formatter
{
    public static class OranizationFormatter
    {
        public static void FormatOrganizationOutput(List<Organization> orgs)
        {
            if(orgs != null)
            {
                foreach (var org in orgs)
                {
                    Console.WriteLine("..................");
                    Console.WriteLine("Id:                 " + org?.Id);
                    Console.WriteLine("Url:                " + org?.Url);
                    Console.WriteLine("External_Id:        " + org?.External_Id);
                    Console.WriteLine("Name:               " + org?.Name);
                    Console.WriteLine("Domain Names:       " + org?.Domain_Names);
                    Console.WriteLine("Created Date:       " + org?.Createdate);
                    Console.WriteLine("Details:            " + org?.Details);
                    Console.WriteLine("Shared Tickets:     " + org?.Shared_Tickets);

                    string tagList = org?.Tags.Aggregate((a, b) => a + "," + b);

                    Console.WriteLine("Tags:               " + tagList);
                    Console.WriteLine("..................");
                }
            }
        }
    }
}
