namespace EventStore.Monitoring.Infrastructure.Models.Http.Stats
{
    public class DiskIo
    {
        public long ReadBytes { get; set; }
        
        public long WrittenBytes { get; set; }
        
        public long ReadOps { get; set; }
        
        public long WriteOps { get; set; }
    }
}