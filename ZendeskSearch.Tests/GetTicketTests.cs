using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using ZendeskSearch.Models;
using ZendeskSearch.Repository;
using ZendeskSearch.Settings;

namespace ZendeskSearch.Tests
{
    public class GetTicketTests
    {
        private readonly IGetTicket _service;
        private readonly Mock<IGetOrganization> _mockGetOrg;
        public GetTicketTests()
        {
            var mockLogger = new Mock<ILogger<GetTicket>>();
            _mockGetOrg = new Mock<IGetOrganization>();
            AppSettings settings = new AppSettings()
            {
                TicketsFilePath = "Data/tickets.json",
            };

            var _mockFilePath = new Mock<IOptions<AppSettings>>();
            _mockFilePath.Setup(ap => ap.Value).Returns(settings);

            _service = new GetTicket(mockLogger.Object, _mockGetOrg.Object, _mockFilePath.Object);
        }

        
        [Theory]
        [InlineData("id", "436bf9b0-1147-4c0a-8439-6f79833bff5b", "116")]
        [InlineData("external_id", "9210cdc9-4bee-485f-a078-35396cd74063", "116")]
        [InlineData("description", "", "116")]
        [InlineData("submitter_id", "3811", "116")]
        [InlineData("assignee_id", "2411", "116")]
        [InlineData("type", "incident-test", "116")]
        [InlineData("subject", "A Catastrophe in Korea (North) for test", "116")]
        [InlineData("url", "http://initech.zendesk.com/api/v2/tickets/436bf9b0-1147-4c0a-8439-6f79833bff5b.json", "116")]
        public void GetTicket_RequestKeyDescriptionWithEmptyValue_ReturnsTicketDetailsWithEmptyDescription(string key, string val, string orgId)
        {
            var serialisedexpectedResponse = ExpectedResult();

            var expectedOrgResponse = ExpectedOrganization(orgId);

            _mockGetOrg.Setup(x => x.GetOrganizationDetails("Id", orgId)).Returns(expectedOrgResponse);

            var actualResponse = _service.GetTicketDetails(key, val);
            var serializedActualResponse = JsonConvert.SerializeObject(actualResponse);

            Assert.Equal(serialisedexpectedResponse, serializedActualResponse);
        }

        private string ExpectedResult()
        {
            using StreamReader r = new StreamReader("Data/ticketResult.json");
            string json = r.ReadToEnd();

            var expectedResponse = new List<TicketSearchEntity>();
            var ticket = JsonConvert.DeserializeObject<Ticket>(json);
            var ticketEntity = new TicketSearchEntity()
            {
                Ticket = ticket,
                OrganizationName = "Zentry"
            };
            expectedResponse.Add(ticketEntity);
            return JsonConvert.SerializeObject(expectedResponse);
        }

        private List<Organization> ExpectedOrganization(string orgId)
        {
            var expectedOrgResponse = new List<Organization>();
            var org = new Organization()
            {
                Id = Convert.ToInt32(orgId),
                Name = "Zentry"
            };
            expectedOrgResponse.Add(org);

            return expectedOrgResponse;
        }
    }
}
