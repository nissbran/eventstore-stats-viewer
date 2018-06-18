using EventStore.Monitoring.Infrastructure.Communication;
using EventStore.Monitoring.Infrastructure.Communication.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Monitoring.Api.Infrastructure.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.Configure<EventStoreHttpOptions>(options =>
            {
                options.Url = "http://localhost:2113";
                options.UserName = "admin";
                options.Password = "changeit";
            });
            
            services.AddSingleton<EventStoreHttpClient>();
        }
    }
}
