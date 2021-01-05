using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ZendeskSearch.Helpers;
using ZendeskSearch.Models;
using ZendeskSearch.Settings;

namespace ZendeskSearch.Repository
{
    public interface IGetOrganization
    {
        List<Organization> GetOrganizationDetails(string key, string value);
    }
    public class GetOrganization : IGetOrganization
    {
        private readonly ILogger<GetOrganization> _logger;
        private readonly string _filePath;

        public GetOrganization(ILogger<GetOrganization> logger, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _filePath = settings.Value.OrganizationsFilePath;
        }

        public List<Organization> GetOrganizationDetails(string key, string value)
        {
            var orgDetails = new List<Organization>();
            var titleCaseKey = TitleCaseString.ToTitleCase(key);

            using StreamReader r = new StreamReader(_filePath);
            string json = r.ReadToEnd();

            var allOrg = JsonConvert.DeserializeObject<List<Organization>>(json);

            foreach (var org in allOrg)
            {
                if (org.GetType().GetTypeInfo().GetDeclaredProperty(titleCaseKey) != null)
                {
                    if (org.GetType().GetProperty(titleCaseKey).GetValue(org)?.ToString() == value)
                    {
                        orgDetails.Add(org);
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid search term for Organizations");
                    _logger.LogInformation($"Invalid search term {key}");
                    orgDetails = null;
                    break;
                }

            }
            return orgDetails;
        }
    }
}
