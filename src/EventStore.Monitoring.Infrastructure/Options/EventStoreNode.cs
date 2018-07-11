namespace EventStore.Monitoring.Infrastructure.Options
{
    public class EventStoreNode
    {
        public string Name { get; set; }
        
        public string Host { get; set; } = "localhost";

        public int HttpPort { get; set; } = 2113;

        public int TcpPort { get; set; } = 1113;
        
        public string StatsStream { get; set; }
    }
}