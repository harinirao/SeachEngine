using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearch.Models
{
    public class Organization
    {
        [JsonProperty(PropertyName = "_id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "external_id")]
        public Guid External_Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "domain_names")]
        public string[] Domain_Names { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime Createdate { get; set; }
        [JsonProperty(PropertyName = "details")]
        public string Details { get; set; }
        [JsonProperty(PropertyName = "shared_tickets")]
        public bool Shared_Tickets { get; set; }
        [JsonProperty(PropertyName = "tags")]
        public string[] Tags { get; set; }
    }
}
