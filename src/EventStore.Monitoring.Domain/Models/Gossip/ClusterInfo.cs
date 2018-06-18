using System;
using System.Collections.Generic;
using System.Text;

namespace EventStore.Monitoring.Domain.Models.Gossip
{
    public class ClusterInfo
    {
        public MemberInfo[] Members { get; set; }
        
        public string ServerIp { get; set; }
        
        public int ServerPort { get; set; }
    }
}
