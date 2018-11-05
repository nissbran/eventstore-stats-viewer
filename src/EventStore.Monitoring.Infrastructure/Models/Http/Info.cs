using System;

namespace EventStore.Monitoring.Infrastructure.Models.Http
{
    public class Info
    { 
        public string ESVersion { get; set; } 
        
        public string State { get; set; } 
        
        public string ProjectionsMode { get; set; } 
        
        public Guid InstanceId { get; set; } 
    }
}