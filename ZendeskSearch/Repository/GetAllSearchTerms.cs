using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearch.Repository
{
    public static class GetAllSearchTerms
    {

        public static void SearchTerms()
        {
            Console.WriteLine(".......................");
            Console.WriteLine(@"Search Users with
id
url
external_id
name
alias
created_at
active
verified
shared
locale
timezone
last_login_at
email
phone
signature
organization_id
tags
suspended
role
");
            Console.WriteLine(".......................");
            Console.WriteLine(@"Search Tickets with
id
url
external_id
created_at
type
subject
description
priority
status
submitterI_id
assigneeid
organization_id
tags
has_incidents
due_at
via");
            Console.WriteLine(".......................");
            Console.WriteLine(@"Search Organisations with
id
url
external_id
name
domain_names
created_at
details
shared_tickets
tags");
        }
    }
}
