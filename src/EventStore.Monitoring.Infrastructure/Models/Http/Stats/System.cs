using EventStore.Monitoring.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace EventStore.Monitoring.Infrastructure.Models.Http.Stats
{
    public class System
    {
        public decimal Cpu { get; set; }
        
        public decimal FreeMem { get; set; }
        
        [JsonConverter(typeof(DrivesJsonConverter))]
        public Drives Drive { get; set; }
    }
}