using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EventStore.Monitoring.Infrastructure.Communication.Models;
using EventStore.Monitoring.Infrastructure.Models.Http;
using EventStore.Monitoring.Infrastructure.Models.Http.Gossip;
using EventStore.Monitoring.Infrastructure.Models.Http.Stats;
using EventStore.Monitoring.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EventStore.Monitoring.Infrastructure.Communication
{
    public class EventStoreHttpClient
    {
        private readonly List<EventStoreHttpNode> _httpNodes = new List<EventStoreHttpNode>();

        public EventStoreHttpClient(IOptions<EventStoreOptions> httpOptions)
        {
            var options = httpOptions.Value;

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{options.UserName}:{options.Password}"));

            if (options.IsCluster)
            {
                foreach (var clusterNode in options.ClusterNodes)
                {
                    _httpNodes.Add(new EventStoreHttpNode(clusterNode, credentials));
                }
            }
            else
            {
                _httpNodes.Add(new EventStoreHttpNode(options.SingleNode, credentials));
            }
        }
        
        public async Task<List<NodeStats>> GetStats()
        {
            var stats = new List<NodeStats>();
            
            foreach (var httpNode in _httpNodes)
            {
                var response = await httpNode.Client.GetAsync("stats");

                var statsMessage = await response.Content.ReadAsStringAsync();

                stats.Add(JsonConvert.DeserializeObject<NodeStats>(statsMessage));
            }

            return stats;
        }

        public async Task<ClusterInfo> GetClusterGossip()
        {
            var response = await _httpNodes.First().Client.GetAsync("gossip");

            var statsMessage = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ClusterInfo>(statsMessage);
        }
    }
}