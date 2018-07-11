using System;

namespace EventStore.Monitoring.Infrastructure.Models.Http.Stats
{
    public class TcpStats
    {
        public int Connections { get; set; }
        
        public decimal ReceivingSpeed { get; set; }
        
        public decimal SendingSpeed { get; set; }
        
        public int InSend { get; set; }
         
        public DateTime MeasureTime { get; set; }
        
        public long PendingReceived { get; set; }
        
        public long PendingSend { get; set; }
        
        public long ReceivedBytesSinceLastRun { get; set; }
        
        public long ReceivedBytesTotal { get; set; }
        
        public long SentBytesSinceLastRun { get; set; }
        
        public long SentBytesTotal { get; set; }
    }
}