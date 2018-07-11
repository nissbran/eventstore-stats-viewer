namespace EventStore.Monitoring.Infrastructure.Models.Http.Gossip
{
    public class ClusterInfo
    {
        public MemberInfo[] Members { get; set; }
        
        public string ServerIp { get; set; }
        
        public int ServerPort { get; set; }
    }
}
