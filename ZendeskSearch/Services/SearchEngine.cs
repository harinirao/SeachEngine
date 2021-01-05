using Newtonsoft.Json;
using System;
using ZendeskSearch.Formatter;
using ZendeskSearch.Repository;

namespace ZendeskSearch.Services
{
    public interface ISearchEngine
    {
        void Search();
    }
    public class SearchEngine : ISearchEngine
    {
        private readonly IGetUser _user;
        private readonly IGetTicket _ticket;
        private readonly IGetOrganization _org;

        public SearchEngine(IGetUser user, IGetTicket ticket, IGetOrganization org)
        {
            _user = user;
            _ticket = ticket;
            _org = org;
        }

        public void Search()
        {
            Console.WriteLine(@"Welcome to Zendesk search engine
Press Ctrl+C to exit at any time, Press enter to continue

Select Search options: 
     * Press 1 to search Zendesk
     * Press 2 to view a list of searchable fields
     * Press Ctrl+C to exit");

            var userInputSelectSearch = Console.ReadLine();
            if (userInputSelectSearch == "1")
            {
                Console.WriteLine(@"Select 1) Users 2) Tickets 3) Organisations");
                var userSearchInput = Console.ReadLine();
                var termAndValueResult = SearchTermAndValue();

                switch (userSearchInput)
                {
                    case "1":
                        Console.WriteLine($"Searching users for Key: {termAndValueResult.term} and Value: {termAndValueResult.val}");
                        var userSearchResult = _user.GetUserDetails(termAndValueResult.term, termAndValueResult.val);
                        UserFormatter.UserFormatOutput(userSearchResult);
                        break;
                    case "2":
                        Console.WriteLine($"Searching tickets for Key: {termAndValueResult.term} and Value: {termAndValueResult.val}");
                        var ticketSearchResult = _ticket.GetTicketDetails(termAndValueResult.term, termAndValueResult.val);
                        TicketFormatter.FormatTicketOutput(ticketSearchResult);
                        break;
                    case "3":
                        Console.WriteLine($"Searching organisations for Key: {termAndValueResult.term} and Value: {termAndValueResult.val}");
                        var orgSearchResult = _org.GetOrganizationDetails(termAndValueResult.term, termAndValueResult.val);
                        OranizationFormatter.FormatOrganizationOutput(orgSearchResult);
                        break;
                    default:
                        Console.WriteLine("Invalid search option");
                        break;
                }

            }
            else if (userInputSelectSearch == "2")
            {
                GetAllSearchTerms.SearchTerms();
            }
            else
            {
                Console.WriteLine("No results found. Please check for valid inputs");
            }
        }

        private (string term, string val) SearchTermAndValue()
        {
            Console.WriteLine("Enter search term");
            var userInputSearchTerm = Console.ReadLine();

            Console.WriteLine("Enter search value");
            var userInputSearchValue = Console.ReadLine();

            return (userInputSearchTerm, userInputSearchValue);
        }
    }
}
