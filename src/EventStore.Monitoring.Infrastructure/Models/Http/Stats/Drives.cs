using System.Collections.Generic;

namespace EventStore.Monitoring.Infrastructure.Models.Http.Stats
{
    public class Drives : List<Drive>
    {
    }

    public class Drive
    {
        public string Name { get; set; } 
        
        public long AvailableBytes { get; set; }
        
        public long TotalBytes { get; set; }
        
        public string Usage { get; set; }
        
        public long UsedBytes { get; set; }
    }
}