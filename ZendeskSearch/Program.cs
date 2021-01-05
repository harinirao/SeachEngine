using Microsoft.Extensions.Hosting;
using System;

namespace ZendeskSearch
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Starting Zendesk Search tool");
            if ((args?.Length ?? 0) > 0)
            {
                Console.WriteLine($"Args: {string.Join(", ", args)}");
            }
            try
            {
                var host = new HostBuilder()
                    .UseConsoleLifetime()
                    .ConfigureAppConfiguration(ConfigureAppConfiguration)
                    .ConfigureServices(ConfigureServices)
                    .ConfigureLogging(ConfigureLogging);
                host.RunConsoleAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
