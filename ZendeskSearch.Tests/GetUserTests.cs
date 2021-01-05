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
    public class GetUserTests
    {
        private readonly IGetUser _service;
        private readonly Mock<IGetOrganization> _mockGetOrg;
        public GetUserTests()
        {
            var mockLogger = new Mock<ILogger<GetUser>>();
            _mockGetOrg = new Mock<IGetOrganization>();
            AppSettings settings = new AppSettings()
            {
                TicketsFilePath = "Data/tickets.json",
                UsersFilePath = "Data/users.json"
            };

            var _mockFilePath = new Mock<IOptions<AppSettings>>();
            _mockFilePath.Setup(ap => ap.Value).Returns(settings);



            _service = new GetUser(mockLogger.Object, _mockGetOrg.Object, _mockFilePath.Object);
        }

        

        [Theory]
        [InlineData("id", "4", "122")]
        [InlineData("Name", "Rose Newton", "122")]
        [InlineData("alias", "Mr Cardenas", "122")]
        [InlineData("url", "http://initech.zendesk.com/api/v2/users/4.json", "122")]
        [InlineData("external_id", "37c9aef5-cf01-4b07-af24-c6c49ac1d1c7", "122")]
        [InlineData("email", "cardenasnewton@flotonic.com", "122")]
        [InlineData("phone", "8685-482-450", "122")]
        [InlineData("signature", "This is a unit test signature", "122")]
        [InlineData("role", "developer", "122")]
        public void GetUser_RequestKeyValue_ReturnsUserDetailsForUserWithRightKeyValue(string key, string val, string orgId)
        {          

            var serialisedexpectedResponse = ExpectedResult();

            var expectedOrgResponse = ExpectedOrganization(orgId);

            _mockGetOrg.Setup(x => x.GetOrganizationDetails("Id", orgId)).Returns(expectedOrgResponse);

            var actualResponse = _service.GetUserDetails(key, val);
            var serializedActualResponse = JsonConvert.SerializeObject(actualResponse);

            Assert.Equal(serialisedexpectedResponse, serializedActualResponse);
        }

        private string ExpectedResult()
        {
            using StreamReader r = new StreamReader("Data/userResult.json");
            string json = r.ReadToEnd();

            var expectedResponse = new List<UserSearchEntity>();
            var user = JsonConvert.DeserializeObject<User>(json);
            var userEntity = new UserSearchEntity()
            {
                User = user,
                OrganizationName = "Geekfarm",
                TicketSubjects = new List<string> { "A Catastrophe in Maldives", "A Catastrophe in US Minor Outlying Islands", "A Drama in Indonesia", "A Nuisance in Chile", "A Catastrophe in Gibraltar", "A Catastrophe in Brazil", "A Nuisance in Bhutan", "A Drama in Canada", "A Nuisance in Uganda", "A Nuisance in United States", "A Catastrophe in Singapore", "A Drama in Kazakhstan", "A Problem in Malawi" }
            };

            expectedResponse.Add(userEntity);

            return JsonConvert.SerializeObject(expectedResponse);
        }

        private List<Organization> ExpectedOrganization(string orgId)
        {
            var expectedOrgResponse = new List<Organization>();
            var org = new Organization()
            {
                Id = Convert.ToInt32(orgId),
                Name = "Geekfarm"
            };
            expectedOrgResponse.Add(org);

            return expectedOrgResponse;
        }
    }
}
