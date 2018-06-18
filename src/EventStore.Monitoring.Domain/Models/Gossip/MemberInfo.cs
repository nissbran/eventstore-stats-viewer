using System;

namespace EventStore.Monitoring.Domain.Models.Gossip
{
    public class MemberInfo
    {
        public Guid InstanceId { get; set; }

        public DateTime TimeStamp { get; set; }
        public NodeState State { get; set; }
        public bool IsAlive { get; set; }

        public string InternalTcpIp { get; set; }
        public int InternalTcpPort { get; set; }
        public int InternalSecureTcpPort { get; set; }

        public string ExternalTcpIp { get; set; }
        public int ExternalTcpPort { get; set; }
        public int ExternalSecureTcpPort { get; set; }

        public string InternalHttpIp { get; set; }
        public int InternalHttpPort { get; set; }

        public string ExternalHttpIp { get; set; }
        public int ExternalHttpPort { get; set; }

        public long LastCommitPosition { get; set; }
        public long WriterCheckpoint { get; set; }
        public long ChaserCheckpoint { get; set; }

        public long EpochPosition { get; set; }
        public int EpochNumber { get; set; }
        public Guid EpochId { get; set; }

        public int NodePriority { get; set; }
    }
}
