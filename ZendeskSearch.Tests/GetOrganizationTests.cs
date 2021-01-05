using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Xunit;
using ZendeskSearch.Models;
using ZendeskSearch.Repository;
using ZendeskSearch.Settings;

namespace ZendeskSearch.Tests
{
    public class GetOrganizationTests
    {
        private readonly IGetOrganization _service;
        public GetOrganizationTests()
        {
            var mockLogger = new Mock<ILogger<GetOrganization>>();
            AppSettings settings = new AppSettings()
            {
                OrganizationsFilePath = "Data/organizations.json",
            };

            var _mockFilePath = new Mock<IOptions<AppSettings>>();
            _mockFilePath.Setup(ap => ap.Value).Returns(settings);

            _service = new GetOrganization(mockLogger.Object, _mockFilePath.Object);
        }

        [Theory]
        [InlineData("id", "101")]
        [InlineData("name", "Enthaze")]
        [InlineData("external_id", "9270ed79-35eb-4a38-a46f-35725197ea8d")]
        [InlineData("url", "http://initech.zendesk.com/api/v2/organizations/101.json")]
        [InlineData("details", "MegaCorp-for-test")]
        public void GetOrganization_RequestKeyIdValue101_ReturnsOrgDetailsForId101(string key, string val)
        {
            using StreamReader r = new StreamReader("Data/organizationResult.json");
            string json = r.ReadToEnd();

            var expectedResponse = new List<Organization>();
            var org = JsonConvert.DeserializeObject<Organization>(json);            
            expectedResponse.Add(org);
            var serialisedexpectedResponse = JsonConvert.SerializeObject(expectedResponse);   

            var actualResponse = _service.GetOrganizationDetails(key, val);
            var serializedActualResponse = JsonConvert.SerializeObject(actualResponse);

            Assert.Equal(serialisedexpectedResponse, serializedActualResponse);
        }
        
    }
}
