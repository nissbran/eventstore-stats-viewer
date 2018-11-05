using EventStore.Monitoring.Infrastructure.Models.Http;
using EventStore.Monitoring.Infrastructure.Models.Http.Gossip;
using EventStore.Monitoring.Infrastructure.Models.Http.Stats;

namespace EventStore.Monitoring.Infrastructure.Models
{
    public class CollectedEventStoreInfoNode
    {
        public Info Info { get; }
        
        public NodeStats Stats { get; }
        
        public MemberInfo MemberInfo { get; }

        public CollectedEventStoreInfoNode(Info info, NodeStats stats, MemberInfo memberInfo)
        {
            Info = info;
            Stats = stats;
            MemberInfo = memberInfo;
        }
    }
}