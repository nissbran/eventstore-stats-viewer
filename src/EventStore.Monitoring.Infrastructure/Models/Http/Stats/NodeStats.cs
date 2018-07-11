namespace EventStore.Monitoring.Infrastructure.Models.Http.Stats
{
    public class NodeStats
    {
        public string Name { get; set; } 
        
        public Process Proc { get; set; }
        
        public System Sys { get; set; }
    }
}