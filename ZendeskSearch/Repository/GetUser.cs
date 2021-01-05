using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ZendeskSearch.Models;
using ZendeskSearch.Helpers;
using Microsoft.Extensions.Options;
using ZendeskSearch.Settings;


namespace ZendeskSearch.Repository
{
    public interface IGetUser
    {
        List<UserSearchEntity> GetUserDetails(string key, string value);
    }
    public class GetUser :IGetUser
    {
        private readonly ILogger<GetUser> _logger;
        private readonly IGetOrganization _getOrg;
        private readonly string _userFilePath;
        private readonly string _ticketFilePath;
        public GetUser(ILogger<GetUser> logger, IGetOrganization getOrg, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _getOrg = getOrg;
            _userFilePath = settings.Value.UsersFilePath;
            _ticketFilePath = settings.Value.TicketsFilePath;

        }
        public List<UserSearchEntity> GetUserDetails(string key, string value)
        {
            var userDetails = new List<UserSearchEntity>();
            var titleCaseKey = TitleCaseString.ToTitleCase(key);

            using StreamReader r = new StreamReader(_userFilePath);
            string json = r.ReadToEnd();
            var allUsers = JsonConvert.DeserializeObject<List<User>>(json);

            foreach (var user in allUsers)
            {
                var mapUser = new UserSearchEntity();
                if (user.GetType().GetTypeInfo().GetDeclaredProperty(titleCaseKey) != null) 
                {
                    if (user.GetType().GetProperty(titleCaseKey).GetValue(user)?.ToString() == value)
                    {
                        if (user?.Organization_Id != 0)
                        {                            
                            var orgDetails = _getOrg.GetOrganizationDetails("Id", user.Organization_Id.ToString());
                            var ticketSubjectDetails = GetTicketsSubject(user.Organization_Id);
                            mapUser.OrganizationName = orgDetails?.FirstOrDefault(r => r.Id == user?.Organization_Id).Name;
                            mapUser.User = user;
                            mapUser.TicketSubjects = ticketSubjectDetails;
                        }
                        userDetails.Add(mapUser);
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid search term");
                    _logger.LogInformation($"Invalid search term {key}");
                    userDetails = null;
                    break;
                }
                    

            }
            return userDetails;

        }       

        private List<string> GetTicketsSubject(int orgId)
        {
            List<string> listofTicketSubjects = new List<string>();
            if (orgId != 0)
            {
                using StreamReader reader = new StreamReader(_ticketFilePath);
                string json = reader.ReadToEnd();
                var allTickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
                foreach (var ticket in allTickets)
                {
                    if (ticket?.Organization_Id == orgId)
                    {
                        listofTicketSubjects.Add(ticket.Subject);
                    }
                }
            }
            return listofTicketSubjects;
        }

    }
}
