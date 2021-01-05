using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZendeskSearch.Models
{

    public class UserSearchEntity
    {
        public User User { get; set; }
        public string OrganizationName { get; set; }
        public List<string> TicketSubjects { get; set; }
    }
    public class User
    {
        [JsonProperty(PropertyName = "_id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "external_id")]
        public Guid External_Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime Created_At { get; set; }
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }
        [JsonProperty(PropertyName = "verified")]
        public bool Verified { get; set; }
        [JsonProperty(PropertyName = "shared")]
        public bool Shared { get; set; }
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }
        [JsonProperty(PropertyName = "timezone")]
        public string Timezone { get; set; }
        [JsonProperty(PropertyName = "last_login_at")]
        public DateTime Last_Login_At { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "signature")]
        public string Signature { get; set; }
        [JsonProperty(PropertyName = "organization_id")]
        public int Organization_Id { get; set; }
        [JsonProperty(PropertyName = "tags")]
        public string[] Tags { get; set; }
        [JsonProperty(PropertyName = "suspended")]
        public bool Suspended { get; set; }
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

    }
}
