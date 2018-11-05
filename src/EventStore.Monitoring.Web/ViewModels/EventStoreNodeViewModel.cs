using System;
using System.Linq;
using EventStore.Monitoring.Infrastructure.Models.Http;
using EventStore.Monitoring.Infrastructure.Models.Http.Stats;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace EventStore.Monitoring.Web.ViewModels
{
    public class EventStoreNodeViewModel
    {   
        public string Name { get; }
        public string EventStoreVersion { get; }
        
        public string ProcessCpuPercentage { get; }
        public string ProcessCpuScaled { get; }
        public string NodeUpTime { get; }
        
        public string SystemCpuPercentage { get; }
        public string SystemFreeMemory { get; }
        public string SystemDataDiskSize { get; }
        public string SystemDataDiskUsage { get; }
        public string SystemDataDiskUsagePercentage { get; }
        
        public EventStoreNodeViewModel(NodeStats stats, Info info)
        {
            Name = stats.Name;
            EventStoreVersion = info.ESVersion;
            
            ProcessCpuPercentage = stats.Proc?.Cpu.ToString("F2");
            ProcessCpuScaled = stats.Proc?.CpuScaled.ToString("F2");
            
            var upTime = (DateTimeOffset.UtcNow - stats.Proc?.StartTime).Value;
            NodeUpTime = $"{upTime.Days} Days {upTime.Hours} Hours {upTime.Minutes} minutes";
            
            SystemCpuPercentage = stats.Sys?.Cpu.ToString("F2");
            SystemFreeMemory = (stats.Sys?.FreeMem / 1e6m)?.ToString("F0");

            var drive = stats.Sys?.Drive.FirstOrDefault();
            SystemDataDiskSize = (drive?.TotalBytes / 1e9m)?.ToString("F2");
            SystemDataDiskUsage = (drive?.UsedBytes / 1e9m)?.ToString("F2");
            SystemDataDiskUsagePercentage = drive?.Usage;
        }
    }
}