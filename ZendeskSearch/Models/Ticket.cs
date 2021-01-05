using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearch.Models
{
    public class TicketSearchEntity
    {
        public Ticket Ticket { get; set; }
        public string OrganizationName { get; set; }
    }
    public class Ticket
    {
        [JsonProperty(PropertyName = "_id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "external_id")]
        public Guid External_Id { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime Created_At { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "priority")]
        public string Priority { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "submitter_id")]
        public int Submitter_Id { get; set; }
        [JsonProperty(PropertyName = "assignee_id")]
        public int Assignee_Id { get; set; }
        [JsonProperty(PropertyName = "organization_id")]
        public int Organization_Id { get; set; }
        [JsonProperty(PropertyName = "tags")]
        public string[] Tags { get; set; }
        [JsonProperty(PropertyName = "has_incidents")]
        public bool Has_Incidents { get; set; }
        [JsonProperty(PropertyName = "due_at")]
        public DateTime Due_At { get; set; }
        [JsonProperty(PropertyName = "via")]
        public string Via { get; set; }
    }
}
