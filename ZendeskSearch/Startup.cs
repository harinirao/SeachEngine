using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ZendeskSearch.Repository;
using ZendeskSearch.Services;
using ZendeskSearch.Settings;

namespace ZendeskSearch
{
    
    public partial class Program
    {
        public static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder config)
        {
            config
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables();
        }

        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            
            services
                .Configure<AppSettings>(context.Configuration.GetSection("AppSettings"))
                .AddSingleton<IGetOrganization, GetOrganization>()
                .AddSingleton<IGetTicket, GetTicket>()
                .AddSingleton<IGetUser, GetUser>()
                .AddSingleton<ISearchEngine, SearchEngine>()
                .AddSingleton<Processor>();

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<Processor>().Run();

        }

        public static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder logging)
        {
            logging.AddConfiguration(context.Configuration.GetSection("Logging"));
            logging.AddDebug();
            logging.AddNLog();
        }
    }
}

