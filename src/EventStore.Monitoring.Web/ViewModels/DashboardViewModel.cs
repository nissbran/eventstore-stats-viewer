using System.Collections.Generic;
using System.Linq;
using EventStore.Monitoring.Infrastructure.Models;
using EventStore.Monitoring.Infrastructure.Models.Http;
using EventStore.Monitoring.Infrastructure.Models.Http.Stats;

namespace EventStore.Monitoring.Web.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<EventStoreNodeViewModel> Nodes { get; }
        
        public DashboardViewModel(List<NodeStats> stats, List<Info> infos)
        {
            Nodes = stats.Select(nodeStats => new EventStoreNodeViewModel(nodeStats, infos.First()));
        }

        public DashboardViewModel(List<CollectedEventStoreInfoNode> collectedEventStoreInfoNodes)
        {
            Nodes = collectedEventStoreInfoNodes.Select(node => new EventStoreNodeViewModel(node.Stats, node.Info));
        }
    }
}