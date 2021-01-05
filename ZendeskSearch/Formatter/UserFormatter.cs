using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZendeskSearch.Models;

namespace ZendeskSearch.Formatter
{
    public static class UserFormatter
    {
        public static void UserFormatOutput(List<UserSearchEntity> users)
        {
            if(users != null)
            {
                foreach (var user in users)
                {
                    Console.WriteLine("..................");
                    Console.WriteLine("Id:                 " + user?.User?.Id);
                    Console.WriteLine("Url:                " + user?.User?.Url);
                    Console.WriteLine("External_Id:        " + user?.User?.External_Id);
                    Console.WriteLine("Name:               " + user?.User?.Name);
                    Console.WriteLine("Alias:              " + user?.User?.Alias);
                    Console.WriteLine("Created_At:         " + user?.User?.Created_At);
                    Console.WriteLine("Active:             " + user?.User?.Active);
                    Console.WriteLine("Verified:           " + user?.User?.Verified);
                    Console.WriteLine("Shared:             " + user?.User?.Shared);
                    Console.WriteLine("Locale:             " + user?.User?.Locale);
                    Console.WriteLine("TimeZone:           " + user?.User?.Timezone);
                    Console.WriteLine("Last_Login_At:      " + user?.User?.Last_Login_At);
                    Console.WriteLine("Email:              " + user?.User?.Email);
                    Console.WriteLine("Phone:              " + user?.User?.Phone);
                    Console.WriteLine("Signature:          " + user?.User?.Signature);
                    Console.WriteLine("Organization_Id:    " + user?.User?.Organization_Id);

                    string tagList = user?.User?.Tags.Aggregate((a, b) => a + "," + b);
                    
                    Console.WriteLine("Tags:               " + tagList);
                    Console.WriteLine("Suspended:          " + user?.User?.Suspended);
                    Console.WriteLine("Role:               " + user?.User?.Role);
                    Console.WriteLine("Organmization_Name: " + user?.OrganizationName);
                    for (int i = 0; i < user?.TicketSubjects?.Count; i++)
                    {
                        Console.WriteLine("Ticket" + i + ":            " + user.TicketSubjects[i]);
                    }
                    Console.WriteLine("..................");
                }
            }
            
        }
    }
}
