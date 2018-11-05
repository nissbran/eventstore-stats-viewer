using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EventStore.Monitoring.Infrastructure.Communication.Models;
using EventStore.Monitoring.Infrastructure.Models;
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

        public async Task<List<Info>> GetInfos()
        {
            var infoMessages = new List<Info>();
            
            foreach (var httpNode in _httpNodes)
            {
                var response = await httpNode.Client.GetAsync("info");

                var infoMessage = await response.Content.ReadAsStringAsync();

                infoMessages.Add(JsonConvert.DeserializeObject<Info>(infoMessage));
            }

            return infoMessages;
        }

        public async Task<ClusterInfo> GetClusterGossip()
        {
            var response = await _httpNodes.First().Client.GetAsync("gossip");

            var statsMessage = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ClusterInfo>(statsMessage);
        }

        public async Task<List<CollectedEventStoreInfoNode>> GetCollectedEventStoreNodes()
        {
            var nodes = new List<CollectedEventStoreInfoNode>();
            var gossip = await GetClusterGossip();

            foreach (var clusterMember in gossip.Members)
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri($"http://{clusterMember.ExternalHttpIp}:{clusterMember.ExternalHttpPort}")
                };
                
                var infoResponseMessage = await client.GetAsync("info");
                var info =  JsonConvert.DeserializeObject<Info>(await infoResponseMessage.Content.ReadAsStringAsync());
                
                var statsResponseMessage = await client.GetAsync("stats");
                var stats =  JsonConvert.DeserializeObject<NodeStats>(await statsResponseMessage.Content.ReadAsStringAsync());
                
                nodes.Add(new CollectedEventStoreInfoNode(info, stats, clusterMember));
            }

            return nodes;
        }
    }
}