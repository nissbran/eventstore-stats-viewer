using System;
using System.Net.Http;
using System.Net.Http.Headers;
using EventStore.Monitoring.Infrastructure.Options;

namespace EventStore.Monitoring.Infrastructure.Communication.Models
{
    public class EventStoreHttpNode
    {
        public string Name { get; }
        
        public HttpClient Client { get; }

        public EventStoreHttpNode(EventStoreNode eventStoreNode, string base64Credentials)
        {
            Name = eventStoreNode.Name;
            
            Client = new HttpClient
            {
                BaseAddress = new Uri($"http://{eventStoreNode.Host}:{eventStoreNode.HttpPort}")
            };
            
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

        }
    }
}