using EventStore.Monitoring.Infrastructure.Communication;
using EventStore.Monitoring.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Monitoring.Web.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EventStoreOptions>(configuration.GetSection("EventStore"));
            
            services.AddSingleton<EventStoreHttpClient>();
            //services.AddSingleton<EventStoreTcpClient>();
        }
    }
}
