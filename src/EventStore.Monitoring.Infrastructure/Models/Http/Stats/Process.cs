using System;

namespace EventStore.Monitoring.Infrastructure.Models.Http.Stats
{
    public class Process
    {
        public DateTimeOffset StartTime { get; set; }
        
        public int Id { get; set; }
        
        public decimal Cpu { get; set; }
        
        public decimal CpuScaled { get; set; }
        
        public int ThreadsCount { get; set; }
        
        public DiskIo DiskIo { get; set; }
        
        public TcpStats Tcp { get; set; }
    }
}