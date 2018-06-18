using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EventStore.Monitoring.Domain.Models.Gossip;
using Newtonsoft.Json;

namespace EventStore.Monitoring.Infrastructure.Communication.Http
{
    public class EventStoreHttpClient
    {
        private readonly HttpClient _httpClient;

        public EventStoreHttpClient(IOptions<EventStoreHttpOptions> httpOptions)
        {
            var options = httpOptions.Value;

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{options.UserName}:{options.Password}"));
            
            _httpClient = new HttpClient();
            
            _httpClient.BaseAddress = new Uri(options.Url);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }
        
        public async Task<dynamic> GetStats()
        {
            var response = await _httpClient.GetAsync("stats");

            var statsMessage = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<dynamic>(statsMessage);
        }

        public async Task<ClusterInfo> GetClusterGossip()
        {
            var response = await _httpClient.GetAsync("cluster/gossip");

            var statsMessage = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ClusterInfo>(statsMessage);
        }
    }
}
