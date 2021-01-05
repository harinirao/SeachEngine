using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ZendeskSearch.Helpers;
using ZendeskSearch.Models;
using ZendeskSearch.Settings;

namespace ZendeskSearch.Repository
{
    public interface IGetTicket
    {
        List<TicketSearchEntity> GetTicketDetails(string key, string value);
    }
    public class GetTicket : IGetTicket
    {
        private readonly ILogger<GetTicket> _logger;
        private readonly IGetOrganization _getOrg;
        private readonly string _filePath;

        public GetTicket(ILogger<GetTicket> logger, IGetOrganization getOrg, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _getOrg = getOrg;
            _filePath = settings.Value.TicketsFilePath;
        }

        public List<TicketSearchEntity> GetTicketDetails(string key, string value)
        {
            var ticketDetails = new List<TicketSearchEntity>();
            var titleCaseKey = TitleCaseString.ToTitleCase(key);

            using StreamReader r = new StreamReader(_filePath);
            string json = r.ReadToEnd();

            var allTickets = JsonConvert.DeserializeObject<List<Ticket>>(json);

            foreach (var ticket in allTickets)
            {
                var mapTicket = new TicketSearchEntity();
                if (ticket.GetType().GetTypeInfo().GetDeclaredProperty(titleCaseKey) != null)
                {
                    if (ticket.GetType().GetProperty(titleCaseKey).GetValue(ticket)?.ToString() == value)
                    {
                        if (ticket?.Organization_Id != 0)
                        {
                            var orgDetails = _getOrg.GetOrganizationDetails("Id", ticket.Organization_Id.ToString());

                            mapTicket.Ticket = ticket;
                            mapTicket.OrganizationName = orgDetails?.FirstOrDefault(r => r.Id  == ticket?.Organization_Id).Name;
                        }
                        ticketDetails.Add(mapTicket);
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid search term for Tickets");
                    _logger.LogInformation($"Invalid search term {key}");
                    ticketDetails = null;
                    break;
                }

            }
            return ticketDetails;

        }
    }
}
