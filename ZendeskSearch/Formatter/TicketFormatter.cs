using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZendeskSearch.Models;

namespace ZendeskSearch.Formatter
{
    public static class TicketFormatter
    {
        public static void FormatTicketOutput(List<TicketSearchEntity> tickets)
        {
            if(tickets != null)
            {
                foreach (var ticket in tickets)
                {
                    Console.WriteLine("..................");
                    Console.WriteLine("Id:                 " + ticket?.Ticket?.Id);
                    Console.WriteLine("Url:                " + ticket?.Ticket?.Url);
                    Console.WriteLine("External_Id:        " + ticket?.Ticket?.External_Id);
                    Console.WriteLine("Created_At:         " + ticket?.Ticket?.Created_At);
                    Console.WriteLine("Type:               " + ticket?.Ticket?.Type);
                    Console.WriteLine("Subject:            " + ticket?.Ticket?.Subject);
                    Console.WriteLine("Description:        " + ticket?.Ticket?.Description);
                    Console.WriteLine("Priority:           " + ticket?.Ticket?.Priority);
                    Console.WriteLine("Status:             " + ticket?.Ticket?.Status);
                    Console.WriteLine("Submitter Id:       " + ticket?.Ticket?.Submitter_Id);
                    Console.WriteLine("Assignee Id:        " + ticket?.Ticket?.Assignee_Id);
                    Console.WriteLine("Organization Id:    " + ticket?.Ticket?.Organization_Id);

                    string tagList = ticket?.Ticket?.Tags.Aggregate((a, b) => a + "," + b);

                    Console.WriteLine("Tags:               " + tagList);
                    Console.WriteLine("Has Incidents:      " + ticket?.Ticket?.Has_Incidents);
                    Console.WriteLine("Due At:             " + ticket?.Ticket?.Due_At);
                    Console.WriteLine("Via:                " + ticket?.Ticket?.Via);
                    Console.WriteLine("Organmization Name: " + ticket?.OrganizationName);
                    Console.WriteLine("..................");
                }
            }
        }
    }
}
