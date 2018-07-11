using System.Collections.Generic;
using System.Linq;
using EventStore.Monitoring.Infrastructure.Models.Http.Stats;

namespace EventStore.Monitoring.Web.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<EventStoreNodeViewModel> Nodes { get; }
        
        public DashboardViewModel(List<NodeStats> stats)
        {
            Nodes = stats.Select(nodeStats => new EventStoreNodeViewModel(nodeStats));
        }
    }
}