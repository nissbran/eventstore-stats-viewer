using EventStore.Monitoring.Infrastructure.Communication;
using EventStore.Monitoring.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace EventStore.Monitoring.Infrastructure.Services
{
    public class InformationService
    {
        private readonly EventStoreHttpClient _eventStoreHttpClient;
        
        public InformationService(IOptions<EventStoreOptions> options)
        {
            _eventStoreHttpClient = new EventStoreHttpClient(options);
        }
        
        
    }
}