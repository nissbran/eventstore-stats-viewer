using System.Collections.Generic;

namespace EventStore.Monitoring.Infrastructure.Options
{
    public class EventStoreOptions
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        
        public bool IsCluster { get; set; }
        
        public EventStoreNode SingleNode { get; set; }
        
        public List<EventStoreNode> ClusterNodes { get; set; }
    }
}
