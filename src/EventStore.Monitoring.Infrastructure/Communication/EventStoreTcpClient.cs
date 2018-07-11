using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using EventStore.Monitoring.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace EventStore.Monitoring.Infrastructure.Communication
{
    public class EventStoreTcpClient
    {
        private readonly IEventStoreConnection _eventStoreConnection;

        private readonly string _statsStream;

        private readonly List<string> _clusterStatsStreams;
        
        public EventStoreTcpClient(IOptions<EventStoreOptions> eventStoreOptions)
        {
            var options = eventStoreOptions.Value;

            var settings = ConnectionSettings.Create()
                .KeepReconnecting()
                .KeepRetrying()
                .FailOnNoServerResponse()
                .SetHeartbeatTimeout(TimeSpan.FromSeconds(2))
                .SetReconnectionDelayTo(TimeSpan.FromSeconds(2))
                .SetDefaultUserCredentials(new UserCredentials(options.UserName, options.Password));

            _statsStream = options.SingleNode.StatsStream;

            _clusterStatsStreams = options.ClusterNodes.Select(node => node.StatsStream).ToList();
            
            _eventStoreConnection = EventStoreConnection.Create($"ConnectTo=tcp://{options.SingleNode.Host}:{options.SingleNode.TcpPort}", settings, "MonitoringWeb");

            _eventStoreConnection.ConnectAsync().Wait();
        }

        public async Task<List<ResolvedEvent>> GetLatestStats()
        {
            var events = await _eventStoreConnection.ReadStreamEventsBackwardAsync(_statsStream, StreamPosition.End, 1, false);

            return events.Events.ToList();
        }

        public async Task ConfigureStatsStream(string statsStream = null)
        {
            var statsStreamName = statsStream ?? _statsStream;
            var statsMetadata = await _eventStoreConnection.GetStreamMetadataAsync(statsStreamName);
            
            if (statsMetadata.StreamMetadata.MaxAge != TimeSpan.FromHours(1))
            {
                await _eventStoreConnection.SetStreamMetadataAsync(
                    statsStreamName,
                    ExpectedVersion.Any,
                    StreamMetadata.Create(maxAge: TimeSpan.FromHours(1)));
            }
        }

        public async Task ConfigureAllClusterStatsStreams(IEnumerable<string> statsStreams = null)
        {
            var statsStreamNames = statsStreams ?? _clusterStatsStreams;

            foreach (var statsStreamName in statsStreamNames)
            {
                await ConfigureStatsStream(statsStreamName);
            }
        }
    }
}