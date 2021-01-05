using Microsoft.Extensions.Logging;
using System;

namespace ZendeskSearch.Services
{
    public class Processor
    {
        private readonly ILogger<Processor> _logger;
        private readonly ISearchEngine _searchEngine;


        public Processor(ILogger<Processor> logger, ISearchEngine searchEngine)
        {
            _logger = logger;
            _searchEngine = searchEngine;
           
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    _logger.LogInformation("Starting search..");
                    _searchEngine.Search();
                    Console.WriteLine("Press Ctrl+C to close the Zendesk search console app");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    _logger.LogError("Couldn't perform search. There was an exception. View logs for more details", e.Message);
                }
            }
            
        }   

    }
}
